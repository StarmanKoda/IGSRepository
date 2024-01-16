using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : EntityScript
{
    [Header("For Patrolling")]
    private float moveDirection = 1f;
    private bool facingRight = true;
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] Transform wallCheckPoint;
    [SerializeField] float circleRadius;
    [SerializeField] LayerMask groundLayer;
    private bool checkingGround;
    private bool checkingWall;
    public bool walksOffLedge;

    

    private Rigidbody2D enemyRB;
    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        checkingGround = Physics2D.OverlapCircle(groundCheckPoint.position, circleRadius, groundLayer);
        checkingWall = Physics2D.OverlapCircle(wallCheckPoint.position, circleRadius, groundLayer);

    }

    void Patrolling()
    {
        if ((checkingWall && !walksOffLedge) || ((checkingWall || !checkingGround) && walksOffLedge))
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
}
