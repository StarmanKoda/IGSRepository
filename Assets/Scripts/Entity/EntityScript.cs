using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityScript : MonoBehaviour
{

    public double health = 100;
    public double maxHealth;
    public float moveSpeed = 1f;
    public double atkDMG = 1f;
    public double knockBackForce = 1500;

    Rigidbody rig;

    public GameObject healthDrop;
    public float lowerPercDrop;
    public float upperPercDrop;
    public bool takingDMG = false;

    public int soundtoPlay = 0;

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
        takingDMG = true;
        SoundManager.Instance.blist[soundtoPlay] = true;
        //WILL NEED TO IMPROVE DEATH EFFECTS
        if (health <= 0)
        {
            if (healthDrop)
            {
                int amount = (int)(maxHealth * Random.Range(lowerPercDrop, upperPercDrop));
                for (int i = HealthDrop.dropAmounts.Length - 1; i >= 0;)
                {
                    if (amount >= HealthDrop.dropAmounts[i])
                    {
                        GameObject drop = Instantiate(healthDrop, transform.position, Quaternion.identity);
                        drop.GetComponent<HealthDrop>().amount = HealthDrop.dropAmounts[i];
                        drop.transform.localScale = Vector3.one * HealthDrop.dropSizes[i];

                        amount -= HealthDrop.dropAmounts[i];

                        drop.transform.parent = transform.parent;
                    }
                    else
                    {
                        i--;
                    }
                }
                //if (amount > 0)
                //{
                //    GameObject drop = Instantiate(healthDrop, transform.position, Quaternion.identity);
                //    drop.GetComponent<HealthDrop>().amount = amount;
                //    drop.transform.localScale = Vector3.one * HealthDrop.dropSizes[0];

                //    amount = 0;

                //    drop.transform.parent = transform.parent;
                //}
            }

            
            Destroy(gameObject);
        }

        return true;
    }

    public virtual void constantDamage(double dps)
    {
        takingDMG = true;
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
