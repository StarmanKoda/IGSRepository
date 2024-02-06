using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WalkingEnemy : EntityScript
{
    [Header("For Patrolling")]
    private float moveDirection = 1f;
    private bool facingRight = true;
    public bool startFacingRight = true;
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] Transform wallCheckPoint;
    [SerializeField] float circleRadius;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask wallLayer;
    private bool checkingGround;
    private bool checkingWall;
    public bool fallsOffLedge;

    [Header("For Damage")]
    private float dmgTimer;
    private float InvincibilityTime = 0.5f;

    [Header("For Stagger Time")]
    private float staggerTimer;
    private float staggerTime = 0.5f;
    private int isStaggered = 0;

    private Rigidbody enemyRB;
    // Start is called before the first frame update
    void Start()
    {
        if (!startFacingRight)
        {
            Flip();
        }
        dmgTimer = InvincibilityTime;
        staggerTimer = staggerTime;
        enemyRB = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (staggerTimer < staggerTime && isStaggered == 0)
        {
            staggerTimer += Time.deltaTime;
        }
        if(isStaggered == 0 && staggerTimer >= staggerTime)
        {
            isStaggered = 1;
            staggerTimer = 0;
        }

        if (dmgTimer < InvincibilityTime)
        {
            dmgTimer += Time.deltaTime;
        }
        checkingGround = Physics.CheckSphere(groundCheckPoint.position, circleRadius, groundLayer);
        checkingWall = Physics.CheckSphere(wallCheckPoint.position, circleRadius, wallLayer);
        Patrolling();
    }

    void Patrolling()
    {
        if ((checkingWall && fallsOffLedge) || ((checkingWall || !checkingGround) && !fallsOffLedge))
        {
            if (facingRight)
            {
                Flip();
            }
            else if (!facingRight)
            {
                Flip();
            }
        }
        enemyRB.velocity = new Vector2(moveSpeed * moveDirection * isStaggered, enemyRB.velocity.y);

        //UnityEngine.Debug.Log("moving!");
    }


    void Flip()
    {
        moveDirection *= -1f;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheckPoint.position, circleRadius);
        Gizmos.DrawWireSphere(wallCheckPoint.position, circleRadius);
    }
    void OnCollisionEnter(Collision coll)
    {
        if ((coll.gameObject.tag == "Player" || coll.gameObject.layer == 6) && (dmgTimer >= InvincibilityTime))
        {
            coll.gameObject.GetComponent<EntityScript>().takeDamage(atkDMG);
            coll.gameObject.GetComponent<Movement>().knockBack(transform, (float)knockBackForce);
            dmgTimer = 0f;
            isStaggered = 0;
            
            
        }
    }

}
