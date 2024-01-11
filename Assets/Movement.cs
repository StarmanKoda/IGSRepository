using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D rig;

    public float speed = 50f;
    float move = 0f;
    float moveSmoothing = .05f;
    Vector3 velocity = Vector3.zero;

    public float airSpd = 50f;

    public float jumpForce = 500f;
    bool jump = false;
    public float preJumpPeriod = 0.2f;
    float preJump = 0;
    public float jumpGracePeriod = 0.2f;
    float jumpGrace = 0;
    public float gravFallMod = 1.5f;
    public float maxFallSpeed = 100f;

    public Transform mesh;

    public LayerMask groundMask;
    public Transform groundCheck;
    public bool grounded;
    float groundedRadius = .2f;

    float gravityScale = 3f;
    
    public bool airControl = false;

    public bool facingRight = true;


    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        gravityScale = rig.gravityScale;
    }

    void Update()
    {
        preJump -= Time.deltaTime;
        jumpGrace -= Time.deltaTime;

        move = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            preJump = preJumpPeriod;
        }

        //if (rig.velocity.y < 0 || (Input.GetButtonUp("Jump") && rig.velocity.y > 0f))
        //{
        //    rig.gravityScale = gravityScale * gravFallMod;

        //    //rig.velocity = new Vector2(rig.velocity.x, Mathf.Max(rig.velocity.y, maxFallSpeed));
        //}

        if (rig.velocity.y < 0 && jumpGrace < 0)
        {
            rig.gravityScale = gravityScale * gravFallMod;

            rig.velocity = new Vector2(rig.velocity.x, Mathf.Max(rig.velocity.y, maxFallSpeed));
        }

        if (Input.GetButtonUp("Jump") && rig.velocity.y > 0f)
        {
            rig.velocity = new Vector2(rig.velocity.x, 0);
        }
    }

    void FixedUpdate()
    {
        bool recentlyGrounded = grounded;

        grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, groundMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                grounded = true;
                rig.gravityScale = gravityScale;
            }
        }

        if (grounded)
        {
            Move(move * speed * Time.deltaTime, jump);
        }
        else
        {
            Move(move * airSpd * Time.deltaTime, jump);

            if (recentlyGrounded)
            {
                jumpGrace = jumpGracePeriod;
            }
        }

        jump = false;
    }

    public void Move(float move, bool jump)
    {
        if (grounded || airControl)
        {
            Vector3 targetVelocity = new Vector2(move * 10f, rig.velocity.y);
            rig.velocity = Vector3.SmoothDamp(rig.velocity, targetVelocity, ref velocity, moveSmoothing);

            if (move > 0 && !facingRight)
            {
                Flip();
            }
            else if (move < 0 && facingRight)
            {
                Flip();
            }
        }
        if ((grounded && (jump || preJump > 0)) || (jumpGrace > 0 && jump))
        {
            grounded = false;
            rig.velocity = new Vector2(rig.velocity.x, 0);
            rig.AddForce(new Vector2(0f, jumpForce));
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;

        // Just flips the player for now can add turn animation later
        Vector3 meshScale = mesh.localScale;
        meshScale.x *= -1;
        mesh.localScale = meshScale;
    }

    public void knockBack(Transform source, float force)
    {
        int dir = 1;
        if (transform.position.x < source.position.x)
        {
            dir = -1;
        }

        rig.AddForce(new Vector2(force * dir, 0));
    }
}
