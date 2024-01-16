using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityScript : MonoBehaviour
{

    public double health = 100;
    public float moveSpeed = 1f;
    public double atkDMG = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(double dmg)
    {
        health -= dmg;

        //WILL NEED TO IMPROVE DEATH EFFECTS
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
