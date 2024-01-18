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

    

    private Rigidbody enemyRB;
    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        checkingGround = Physics.CheckSphere(groundCheckPoint.position, circleRadius, groundLayer);
        checkingWall = Physics.CheckSphere(wallCheckPoint.position, circleRadius, groundLayer);
        Patrolling();
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
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Player" || coll.gameObject.layer == 6)
        {
            coll.gameObject.GetComponent<EntityScript>().health -= atkDMG;
        }
    }

}
