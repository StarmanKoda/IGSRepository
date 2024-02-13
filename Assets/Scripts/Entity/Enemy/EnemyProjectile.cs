using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyProjectile : MonoBehaviour
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
        ProjRB = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 direction = player.transform.position - transform.position;
        ProjRB.velocity = new Vector2(direction.x, direction.y).normalized * moveSpeed;
        float rot = Mathf.Atan2(-direction.x, -direction.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);
    }
    private void FixedUpdate()
    {
        

        
    }
    void OnCollisionEnter(Collision coll)
    {
        if ((coll.gameObject.tag == "Player" || coll.gameObject.layer == 6))
        {
            coll.gameObject.GetComponent<EntityScript>().takeDamage(atkDMG);
            coll.gameObject.GetComponent<Movement>().knockBack(transform, (float)knockBackForce);
            Destroy(gameObject);

        }
        if(coll.gameObject.layer == 3)
        Destroy(gameObject);
    }
}
