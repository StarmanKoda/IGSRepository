using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WallClimbEnemy : EntityScript
{
    private Rigidbody enemyRB;
    public bool startLeft;
    private int startDir;
    private bool checkingGround;
    private bool checkingWall;
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] Transform wallCheckPoint;
    public float groundCheckDistance;
    public float wallCheckDistance;
    [SerializeField] LayerMask groundLayer;
    private bool hasTurn = false;
    private float zAxisAdd;
    private int direction = 1;

    [Header("For Damage")]
    public float dmgTimer;
    public float InvincibilityTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        if (startLeft)
        {
            startDir = -1;
        }
        else
            startDir = 1;
        enemyRB = GetComponent<Rigidbody>();
    }

 

    private void FixedUpdate()
    {
        Movement();
        CheckGroundOrWall();
        if (dmgTimer < InvincibilityTime)
        {
            dmgTimer += Time.deltaTime;
        }
    }

    void CheckGroundOrWall()
    {
        
        checkingGround = Physics.Raycast(groundCheckPoint.position, -transform.up, groundCheckDistance, groundLayer);
        checkingWall = Physics.Raycast(wallCheckPoint.position, transform.right, wallCheckDistance, groundLayer);

        if(!checkingGround)
        {
            if (hasTurn == false)
            {
                zAxisAdd -= 90;
                transform.eulerAngles = new Vector3(0, 0, zAxisAdd);
                if (direction == 1)
                {
                    
                    transform.position = new Vector2(transform.position.x + 0.3f, transform.position.y - 0.3f);
                    //Debug.Log("Turning!");
                    direction = 2;
                    hasTurn = true;
                }
                else if (direction == 2)
                {
                    transform.position = new Vector2(transform.position.x - 0.3f, transform.position.y - 0.3f);
                    //Debug.Log("Turning!");
                    direction = 3;
                    hasTurn = true;
                }
                else if (direction == 3)
                {

                    transform.position = new Vector2(transform.position.x - 0.3f, transform.position.y + 0.3f);
                    //Debug.Log("Turning!");
                    direction = 4;
                    hasTurn = true;
                }
                else if (direction == 4)
                {

                    transform.position = new Vector2(transform.position.x + 0.3f, transform.position.y + 0.3f);
                    //Debug.Log("Turning!");
                    direction = 1;
                    hasTurn = true;
                }
            }
        }
        if(checkingGround)
        {
            hasTurn = false;
        }

        if(checkingWall)
        {
            if (hasTurn == false)
            {
                zAxisAdd += 90;
                transform.eulerAngles = new Vector3(0, 0, zAxisAdd);
                if (direction == 1)
                {

                    transform.position = new Vector2(transform.position.x, transform.position.y);
                    //Debug.Log("Turning from 1 to 4");
                    hasTurn = true;
                    direction = 4;

                }
                else if (direction == 2)
                {

                    transform.position = new Vector2(transform.position.x, transform.position.y);
                    //Debug.Log("Turning from 2 to 1");
                    hasTurn = true;
                    direction = 1;
                    
                }
                else if (direction == 3)
                {

                    transform.position = new Vector2(transform.position.x, transform.position.y);
                    //Debug.Log("Turning from 3 to 2");
                    hasTurn = true;
                    direction = 2;
                }
                else if (direction == 4)
                {

                    transform.position = new Vector2(transform.position.x, transform.position.y);
                    //Debug.Log("Turning from 4 to 3");
                    hasTurn = true;
                    direction = 3;
                    
                }
            }
        }

    }
    void Movement()
    {
        enemyRB.velocity = transform.right * moveSpeed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheckPoint.position, new Vector2(groundCheckPoint.position.x, groundCheckPoint.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheckPoint.position, new Vector2(wallCheckPoint.position.x + wallCheckDistance, wallCheckPoint.position.y));
    }
    
    void OnCollisionEnter(Collision coll)
    {
        if ((coll.gameObject.tag == "Player" || coll.gameObject.layer == 6) && (dmgTimer >= InvincibilityTime))
        {
            coll.gameObject.GetComponent<EntityScript>().takeDamage(atkDMG);
            coll.gameObject.GetComponent<Movement>().knockBack(transform, (float)knockBackForce);
            dmgTimer = 0f;
        }
    }
}
