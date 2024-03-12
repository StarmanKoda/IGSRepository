using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BossEntityScript : MonoBehaviour
{

    public double health = 1000;
    public double maxHealth;
    public float[] moveSpeed = { 1f };
    public double[] atkDMG = { 1 };
    public double knockBackForce = 1500;
    public GameObject bossDrop;
    public UpgradeEnum upgradeType;
    public bool takingDMG = false;
    public float counter = 0f;
    private int deathSound;



    Rigidbody rig;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(takingDMG)
        {
            counter += Time.deltaTime;  
        }
        if(counter >= 0.5f)
        {
            takingDMG = false;
        }
    }

    public virtual bool takeDamage(double dmg)
    {
        health -= dmg;
        takingDMG = true;
        //WILL NEED TO IMPROVE DEATH EFFECTS
        if (health <= 0)
        {
            //SoundManager.Instance.blist[soundtoPlay] = true;
            GameObject drop = Instantiate(bossDrop, transform.position, Quaternion.identity);
            drop.GetComponent<UpgradeUnlocker>().Upg = upgradeType;
            Destroy(gameObject);
        }
        return true;
    }

    public virtual void constantDamage(double dps)
    {
        health -= dps * Time.deltaTime;

        if (health <= 0)
        {
            //SoundManager.Instance.blist[soundtoPlay] = true;
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
