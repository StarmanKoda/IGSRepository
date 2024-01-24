using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class JumpingEnemy : EntityScript
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

    [Header("For Jumping")]
    [SerializeField] float jumpHeight;
    [SerializeField] Transform player;
    [SerializeField] Transform groundCheck;
    [SerializeField] Vector2 boxSize;
    public bool trackPlayerJump;
    public bool HopperType;
    public float timeToJump;
    private float jumpTimer;
    private bool isGrounded;

    [Header("For Damage")]
    private float dmgTimer;
    private float InvincibilityTime = 0.5f;
    

    private Rigidbody enemyRB;
    // Start is called before the first frame update
    void Start()
    {
        if (!startFacingRight)
        {
            Flip();
        
        }
        enemyRB = GetComponent<Rigidbody>();
        dmgTimer = InvincibilityTime;
        if (player == null)
        {
            player = gameObject.transform.Find("Player");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        checkingGround = Physics.CheckSphere(groundCheckPoint.position, circleRadius, groundLayer);
        checkingWall = Physics.CheckSphere(wallCheckPoint.position, circleRadius, wallLayer);
        isGrounded = Physics.CheckBox(groundCheck.position, boxSize, Quaternion.identity, groundLayer);
        jumpTimer += Time.deltaTime;
        
        if (dmgTimer < InvincibilityTime)
        {
            dmgTimer += Time.deltaTime;
        }

        if (!HopperType)
        { 
            Patrolling(); 
        }
        else
        {
            PatrolHop();
        }
        if (Input.GetButtonDown("Jump") && trackPlayerJump)
        {
            Jump();
        }
        else if(!trackPlayerJump && jumpTimer >= timeToJump)
        {
            jumpTimer = 0;
            Jump();
        }
    }
    
    void Patrolling()
    {
        if ( checkingWall)
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
        enemyRB.velocity = new Vector2(moveSpeed * moveDirection, enemyRB.velocity.y);
        //UnityEngine.Debug.Log("moving!");
    }

    void PatrolHop()
    {
        if (checkingWall || !checkingGround)
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
    }

    void Jump()
    {
        float distancefromPlayer = player.position.x - transform.position.x;

        if (isGrounded)
        {
            enemyRB.AddForce(new Vector2(distancefromPlayer, jumpHeight), ForceMode.Impulse);
        }
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
        Gizmos.color = Color.green;
        Gizmos.DrawCube(groundCheck.position, boxSize);
    }

    void OnCollisionEnter(Collision coll)
    {
        if ((coll.gameObject.tag == "Player" || coll.gameObject.layer == 6)&& (dmgTimer >=InvincibilityTime))
        {
            coll.gameObject.GetComponent<EntityScript>().takeDamage(atkDMG);
            coll.gameObject.GetComponent<Movement>().knockBack(transform, (float)knockBackForce);
        }
    }
}
