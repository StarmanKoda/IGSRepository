using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerEnemy : EntityScript
{
    [Header("For Charging")]
    private float moveDirection = 1f;
    private bool facingRight = true;
    public bool startFacingRight = true;
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] Transform wallCheckPoint;
    [SerializeField] Transform enemyEyes;
    [SerializeField] float circleRadius = 0.1f;
    [SerializeField] float sightRadius = 0.5f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] LayerMask playerLayer;
    private bool checkingGround;
    private bool checkingWall;
    private bool checkingPlayer;
    private bool isCharging = false;
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
        staggerTimer = staggerTime;
        dmgTimer = InvincibilityTime;
        enemyRB = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (staggerTimer < staggerTime && isStaggered == 0)
        {
            staggerTimer += Time.deltaTime;
        }
        if (isStaggered == 0 && staggerTimer >= staggerTime)
        {
            isStaggered = 1;
            staggerTimer = 0;
        }

        if (dmgTimer < InvincibilityTime)
        {
            dmgTimer += Time.deltaTime;
        }
        
        checkingGround = Physics.CheckSphere(groundCheckPoint.position, circleRadius * gameObject.transform.localScale.magnitude, groundLayer);
        checkingWall = Physics.CheckSphere(wallCheckPoint.position, circleRadius * gameObject.transform.localScale.magnitude, wallLayer);
        if (facingRight)
            checkingPlayer = Physics.Raycast(enemyEyes.position, transform.right, sightRadius, playerLayer);
        else
            checkingPlayer = Physics.Raycast(enemyEyes.position, transform.right, sightRadius, playerLayer);


        FlipCheck();

        //incase there's a transition animation
        if (checkingPlayer)
        {
            isCharging = true;
        }

        if(isCharging)
        {
            enemyRB.velocity = new Vector2(moveSpeed * moveDirection * isStaggered, enemyRB.velocity.y);
        }
        

    }

    void FlipCheck()
    {
        if ((checkingWall && fallsOffLedge) || ((checkingWall || !checkingGround) && !fallsOffLedge))
        {
            if (facingRight)
            {
                Flip();
                if (isCharging)
                    isCharging = !isCharging;
            }
            else if (!facingRight)
            {
                Flip();
                if (isCharging)
                    isCharging = !isCharging;
            }
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
        Gizmos.DrawWireSphere(groundCheckPoint.position, circleRadius * gameObject.transform.localScale.magnitude);
        Gizmos.DrawWireSphere(wallCheckPoint.position, circleRadius * gameObject.transform.localScale.magnitude);
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
    //private void OnTriggerEnter(Collider other)
    //{
    //    if ((other.gameObject.tag == "Player" || other.gameObject.layer == 6) && (dmgTimer >= InvincibilityTime))
    //    {
    //        isStaggered = 0;

    //    }
    //}
    private void OnDrawGizmos()
    {
        if(facingRight)
        Gizmos.DrawLine(enemyEyes.position, new Vector2(enemyEyes.position.x + sightRadius, enemyEyes.position.y));
        else
        Gizmos.DrawLine(enemyEyes.position, new Vector2(enemyEyes.position.x - sightRadius, enemyEyes.position.y));
    }
}
