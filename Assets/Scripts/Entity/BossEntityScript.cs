using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEntityScript : MonoBehaviour
{

    public double health = 100;
    public double maxHealth;
    public float[] moveSpeed;
    public double[] atkDMG;
    public double knockBackForce = 1500;
    


    Rigidbody rig;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual bool takeDamage(double dmg)
    {
        health -= dmg;

        //WILL NEED TO IMPROVE DEATH EFFECTS
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        return true;
    }

    public virtual void constantDamage(double dps)
    {
        health -= dps * Time.deltaTime;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
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
