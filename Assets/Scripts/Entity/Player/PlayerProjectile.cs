using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerProjectile : MonoBehaviour
{

    public float atkDMG = 3f;
    public float moveSpeed = 100f;
    public float life = 100;
    Vector2 moveDirection;

    

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

        moveDirection = new Vector2(x, y).normalized * moveSpeed;
    }

    private void Update()
    {
        Vector3 newPosition = new Vector3(moveDirection.x * Time.deltaTime * Time.timeScale * moveSpeed, moveDirection.y * Time.deltaTime * Time.timeScale * moveSpeed, 0);
        transform.position += newPosition;
        life -= Time.deltaTime;
        if(life < 0)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider col)
    {
        
        if (col.gameObject.tag.ToLower().Equals("player") || col.gameObject.tag.ToLower().Equals("melee")) return;
        if (col.gameObject.layer == 7)
        {
            col.gameObject.GetComponent<EntityScript>().takeDamage(atkDMG);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
