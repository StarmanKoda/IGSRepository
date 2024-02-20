using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerProjectile : MonoBehaviour
{

    private GameObject player;
    public float atkDMG = 3f;
    public float knockBackForce = 1500;
    public Rigidbody ProjRB;
    public float moveSpeed = 3f;


    public int moveDirectionX;
    public int moveDirectionY;

    

    private void Start()
    {
        float y = Input.GetAxisRaw("Vertical");
        float x = Input.GetAxisRaw("Horizontal");
        if (y == 0 && x == 0)
        {
            //Attack straight in direction facing
            if (Movement.getinstance().facingRight)
            {
                x = 1;
            }
            else
            {
                x = -1;
            }
        }

        ProjRB.velocity = new Vector2(x, y).normalized * moveSpeed;
        float rot = Mathf.Atan2(-x, -y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag.ToLower().Equals("player")) return;
        if (col.gameObject.layer.ToString().ToLower().Equals("enemy"))
        {

            col.gameObject.GetComponent<EntityScript>().takeDamage(atkDMG);
            col.gameObject.GetComponent<Movement>().knockBack(transform, (float)knockBackForce);
            Destroy(gameObject);
        }
        if (col.gameObject.layer == 3 || col.gameObject.layer == 7)
        {
            Destroy(gameObject);
        }
    }
}
