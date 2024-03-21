using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum direction
{
    UP, DOWN, LEFT, RIGHT
}

public class Melee : MonoBehaviour
{
    public LayerMask hitMask;
    
    EntityScript playerEntity;

    public Movement movement;
    public Collider meleeL;
    public Collider meleeR;
    public Collider meleeU;
    public Collider meleeD;
    public SpriteRenderer spriteL;
    public SpriteRenderer spriteR;
    public SpriteRenderer spriteU;
    public SpriteRenderer spriteD;
    public bool noPogo = false;

    direction dir = direction.LEFT;

    public float attkDel;  //Time between player input and attack damaging
    public float attkDur;  //Time attack collider is active
    public float attkRate; //Time between attack end and next attack
    public float nextAttack = 0;

    double damage = 20;

    Rigidbody2D playerRig;
    bool knockBack;
    bool knocked = false;
    public float knockBackForce;

    List<Collider> hits = new List<Collider>();

    // Start is called before the first frame update
    void Start()
    {
        playerRig = GetComponent<Rigidbody2D>();
        damage = GetComponent<EntityScript>().atkDMG;
    }

    // Update is called once per frame
    void Update()
    {

        nextAttack -= Time.deltaTime;

        if (nextAttack < 0)
        {
            if (Input.GetButtonDown("Attack1")) //"Attack1"
            {

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
        SoundManager.Instance.blist[1] = true;
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
        knocked = false;
        movement.pogoing = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (noPogo) return;
        if ((hitMask & (1 << other.gameObject.layer)) == 0 || hits.Contains(other)) //other.gameObject.layer == 6
        {
            return;
        }

        hits.Add(other);

        if (knockBack && !knocked)
        {
            movement.knockBack(other.transform, knockBackForce);
            knocked = true;
        }

        EntityScript entity = other.GetComponent<EntityScript>();
        if (entity)
        {
            entity.takeDamage(damage);
            SoundManager.Instance.blist[6] = true;
        }

        BossEntityScript boss = other.GetComponent<BossEntityScript>();
        if (boss)
        {
            boss.takeDamage(damage);
            SoundManager.Instance.blist[6] = true;
        }

        Spikes spike = other.GetComponent<Spikes>();
        if (dir == direction.DOWN && !movement.pogoing && (entity || spike || boss)) //ADD ANY OTHER POGO OBJECTS (e.g. spikes)
        {
            movement.pogo();
        }
    }
}
