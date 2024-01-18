using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public EntityScript playerEntity;

    public Movement movement;
    public Collider meleeL;
    public Collider meleeR;
    public Collider meleeU;
    public Collider meleeD;
    public SpriteRenderer spriteL;
    public SpriteRenderer spriteR;
    public SpriteRenderer spriteU;
    public SpriteRenderer spriteD;

    public enum direction
    {
        UP, DOWN, LEFT, RIGHT
    }

    direction dir = direction.LEFT;

    public float attkDel;  //Time between player input and attack damaging
    public float attkDur;  //Time attack collider is active
    public float attkRate; //Time between attack end and next attack
    float nextAttack = 0;

    public double damage = 20;

    Rigidbody2D playerRig;
    bool knockBack;
    public float knockBackForce;

    List<Collider> hits = new List<Collider>();

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
            if (Input.GetButtonDown("Attack1")) //"Attack1"
            {
                // GetAxisRaw works fine for keyboard but may need to be changed for controller, not sure
                //float upDown = Input.GetAxisRaw("Vertical");
                //if (upDown < -0.5f && !movement.grounded)
                //{
                //    //Down
                //    dir = direction.DOWN;
                //}
                //else if (upDown > 0.5f)
                //{
                //    //Up
                //    dir = direction.UP;
                //}
                //else if (movement.facingRight)
                //{
                //    //Right
                //    dir = direction.RIGHT;
                //}
                //else
                //{
                //    //Left
                //    dir = direction.LEFT;
                //}

                // This can be easily updated to use animation key frame events istead of invoke

                Invoke("enableAttk", attkDel);
                Invoke("disableAttk", attkDel + attkDur);

                nextAttack = attkDel + attkDur + attkRate;
            }
        }
    }

    public void enableAttk()
    {
        float upDown = Input.GetAxisRaw("Vertical");
        if (upDown < -0.5f && !movement.grounded)
        {
            dir = direction.DOWN;
        }
        else if (upDown > 0.5f)
        {
            dir = direction.UP;
        }
        else if (movement.facingRight)
        {
            dir = direction.RIGHT;
        }
        else
        {
            dir = direction.LEFT;
        }

        if (dir == direction.LEFT)
        {
            meleeL.enabled = true;
            knockBack = true;
            spriteL.enabled = true;
        }
        else if (dir == direction.RIGHT)
        {
            meleeR.enabled = true;
            knockBack = true;
            spriteR.enabled = true;
        }
        else if (dir == direction.UP)
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
        if (dir == direction.LEFT)
        {
            meleeL.enabled = false;
            spriteL.enabled = false;
        }
        else if (dir == direction.RIGHT)
        {
            meleeR.enabled = false;
            spriteR.enabled = false;
        }
        else if (dir == direction.UP)
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == gameObject.layer || hits.Contains(other))
        {
            return;
        }

        hits.Add(other);

        if (knockBack)
        {
            movement.knockBack(other.transform, knockBackForce);
        }

        EntityScript entity = other.GetComponent<EntityScript>();

        if (entity)
        {
            entity.takeDamage(damage);
        }

        if (dir == direction.DOWN && entity) //ADD ANY OTHER POGO OBJECTS (e.g. spikes)
        {
            movement.pogo();
        }
    }
}
