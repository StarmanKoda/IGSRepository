using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public Movement movement;
    public Collider2D meleeL;
    public Collider2D meleeR;
    public Collider2D meleeU;
    public Collider2D meleeD;
    public SpriteRenderer spriteL;
    public SpriteRenderer spriteR;
    public SpriteRenderer spriteU;
    public SpriteRenderer spriteD;
    int direction = 0;

    public float attkDel;  //Time between player input and attack damaging
    public float attkDur;  //Time attack collider is active
    public float attkRate; //Time between attack end and next attack
    float nextAttack = 0;

    public int damage = 1;

    Rigidbody2D playerRig;
    bool knockBack;
    public float knockBackForce;

    List<Collider2D> hits = new List<Collider2D>();

    // Start is called before the first frame update
    void Start()
    {
        playerRig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        nextAttack -= Time.deltaTime;

        if (nextAttack < 0)
        {
            if (Input.GetButtonDown("Fire1")) //"Attack1"
            {
                // GetAxisRaw works fine for keyboard but may need to be changed for controller, not sure
                float upDown = Input.GetAxisRaw("Vertical");
                if (upDown < -0.5f && !movement.grounded)
                {
                    //Down
                    direction = 3;
                }
                else if (upDown > 0.5f)
                {
                    //Up
                    direction = 2;
                }
                else if (movement.facingRight)
                {
                    //Right
                    direction = 1;
                }
                else
                {
                    //Left
                    direction = 0;
                }

                // This can be easily updated to use animation key frame events istead of invoke

                Invoke("enableAttk", attkDel);
                Invoke("disableAttk", attkDel + attkDur);

                nextAttack = attkDel + attkDur + attkRate;
            }
        }
    }

    public void enableAttk()
    {
        if (direction == 0)
        {
            meleeL.enabled = true;
            knockBack = true;
            spriteL.enabled = true;
        }
        else if (direction == 1)
        {
            meleeR.enabled = true;
            knockBack = true;
            spriteR.enabled = true;
        }
        else if (direction == 2)
        {
            meleeU.enabled = true;
            spriteU.enabled = true;
        }
        else
        {
            meleeD.enabled = true;
            spriteD.enabled = true;
        }
    }

    public void disableAttk()
    {
        if (direction == 0)
        {
            meleeL.enabled = false;
            spriteL.enabled = false;
        }
        else if (direction == 1)
        {
            meleeR.enabled = false;
            spriteR.enabled = false;
        }
        else if (direction == 2)
        {
            meleeU.enabled = false;
            spriteU.enabled = false;
        }
        else
        {
            meleeD.enabled = false;
            spriteD.enabled = false;
        }

        hits.Clear();
        knockBack = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == gameObject.layer || hits.Contains(collision))
        {
            return;
        }

        hits.Add(collision);

        if (knockBack)
        {
            movement.knockBack(collision.transform, knockBackForce);
        }

        //if (direction == 3)
        //{
        //    movement.pogo();
        //}

        //ADD DAMAGE AND SUCH FOR ENEMY
    }
}
