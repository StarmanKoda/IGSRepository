using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : EntityScript
{
    public float invincTime = 0.5f;
    float invincOver = 0.5f;
    bool invincible = false;

    public Animator dmgAnim;

    // Start is called before the first frame update
    void Start()
    {
        dmgAnim.speed = 1 / invincTime;
        Physics.IgnoreLayerCollision(6, 7, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (invincible)
        {
            invincOver += Time.deltaTime;
            invincible = invincOver < invincTime;
            if (!invincible)
            {
                dmgAnim.SetBool("Invincible", invincible);
                Physics.IgnoreLayerCollision(6, 7, false);
            }
        }
    }

    public override void takeDamage(double dmg)
    {
        if (!invincible)
        {
            base.takeDamage(dmg);
            invincOver = 0;
            invincible = true;
            dmgAnim.SetBool("Invincible", invincible);
            Physics.IgnoreLayerCollision(6, 7, true);
        }
    }
}
