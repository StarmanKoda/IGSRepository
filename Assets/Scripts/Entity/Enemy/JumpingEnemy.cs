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
    [SerializeField] Transform wallCheckPoint2;
    [SerializeField] Transform wallCheckPoint3;
    [SerializeField] Transform wallCheckPoint4;
    [SerializeField] float circleRadius;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask wallLayer;
    private bool checkingGround;
    private bool checkingWall;
    private bool checkingWall2;
    private bool checkingWall3;
    private bool checkingWall4; 

    [Header("For Jumping")]
    [SerializeField] float jumpHeight;
    [SerializeField] Transform player;
    [SerializeField] Transform groundCheck;
    [SerializeField] Vector2 boxSize;
    public bool trackPlayerJump;
    public bool HopperType;
    public float maxTimeToJump = 3f;
    private float timeToJump;
    private float jumpTimer;
    private bool isGrounded;
    private bool isFalling;

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
        timeToJump = Random.Range(1.5f, maxTimeToJump);
        enemyRB = GetComponent<Rigidbody>();
        dmgTimer = InvincibilityTime;
        staggerTimer = staggerTime;
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
        checkingWall2 = Physics.CheckSphere(wallCheckPoint2.position, circleRadius, wallLayer);
        checkingWall3 = Physics.CheckSphere(wallCheckPoint3.position, circleRadius, wallLayer);
        checkingWall4 = Physics.CheckSphere(wallCheckPoint4.position, circleRadius, wallLayer);

        if (isGrounded)
        {
            jumpTimer += Time.deltaTime;
            isFalling = false;
        }
        
        if (dmgTimer <= InvincibilityTime)
        {
            dmgTimer += Time.deltaTime;
        }
        if (staggerTimer < staggerTime && isStaggered == 0 && isGrounded)
        {
            staggerTimer += Time.deltaTime;
        }
        if (isStaggered == 0 && staggerTimer >= staggerTime)
        {
            isStaggered = 1;
            staggerTimer = 0;
        }

        if(isFalling)
        {
            enemyRB.AddForce(new Vector2(enemyRB.velocity.x, -jumpHeight), ForceMode.Impulse);
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
        else if(!trackPlayerJump && jumpTimer >= timeToJump && isStaggered != 0)
        {
            jumpTimer = 0;
            Jump();
            timeToJump = Random.Range(1.5f, maxTimeToJump);
        }
    }
    
    void Patrolling()
    {
        if ( checkingWall || checkingWall2 || checkingWall3 || checkingWall4)
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
        if(!checkingWall || !checkingWall2 || !checkingWall3 || checkingWall4)
        enemyRB.velocity = new Vector2(moveSpeed * moveDirection * isStaggered, enemyRB.velocity.y);
        //UnityEngine.Debug.Log("moving!");
    }

    void PatrolHop()
    {
        if (checkingWall || checkingWall2 || checkingWall3 || checkingWall4 || !checkingGround)
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
            if(trackPlayerJump)
            enemyRB.AddForce(new Vector2(distancefromPlayer, jumpHeight), ForceMode.Impulse);
            else
            enemyRB.AddForce(new Vector2(enemyRB.velocity.x, jumpHeight), ForceMode.Impulse);
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
        Gizmos.DrawWireSphere(wallCheckPoint2.position, circleRadius);
        Gizmos.DrawWireSphere(wallCheckPoint3.position, circleRadius);
        Gizmos.DrawWireSphere(wallCheckPoint4.position, circleRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawCube(groundCheck.position, boxSize);
    }

    void OnCollisionEnter(Collision coll)
    {
        if ((coll.gameObject.tag == "Player" || coll.gameObject.layer == 6)&& (dmgTimer >=InvincibilityTime))
        {
            coll.gameObject.GetComponent<EntityScript>().takeDamage(atkDMG);
            coll.gameObject.GetComponent<Movement>().knockBack(transform, (float)knockBackForce);
            dmgTimer = 0f;
            isStaggered = 0;
            if (!isGrounded)
            {
                isFalling = true;
            }
        }
    }
}
