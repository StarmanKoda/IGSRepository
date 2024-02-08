using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public double dmg;
    public float force;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        Movement move = collision.gameObject.GetComponent<Movement>();
        if (move)
        {
            bool damaged = move.GetComponent<EntityScript>().takeDamage(dmg);
            Rigidbody rig = move.GetComponent<Rigidbody>();
            if (rig && damaged)
            {
                rig.velocity = new Vector3(0, -.1f, 0);

                Vector3 point = collision.contacts[0].normal;
                Vector3 dir = -transform.up;

                if (point.y < 0) //Hit top
                {
                    dir = transform.up;
                }
                else if (point.x < 0) //Hit right
                {
                    dir = transform.right;
                }
                else if (point.x > 0) //Hit left
                {
                    dir = -transform.right;
                }
                Vector2 forceDir = new Vector2(dir.x * force, 0);
                if (dir.y > 0)
                {
                    forceDir.y = dir.y * force / 2;
                }
                rig.AddForce(forceDir);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Movement move = other.gameObject.GetComponent<Movement>();
        if (move)
        {
            bool damaged = move.GetComponent<EntityScript>().takeDamage(dmg);
            if (damaged)
            {
                move.knockBack(transform, force);
            }
        }
    }
}
