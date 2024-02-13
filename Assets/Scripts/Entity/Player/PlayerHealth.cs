using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : EntityScript
{
    public float invincTime = 0.5f;
    float invincOver = 0.5f;
    bool invincible = false;

    public Animator dmgAnim;

    public Slider healthBar;

    private void OnDestroy()
    {
        if (GetComponent<EntityScript>().health <= 0)
        {
            SceneManager.LoadScene("PlayTestZone0");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        dmgAnim.speed = 1 / invincTime;
        Physics.IgnoreLayerCollision(6, 7, false);

        healthBar = FindObjectOfType<Slider>();
        if (healthBar)
        {
            healthBar.maxValue = (float)maxHealth;
            healthBar.value = (float)health;
        }
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

    public override bool takeDamage(double dmg)
    {
        if (!invincible)
        {
            base.takeDamage(dmg);
            if (healthBar)
            {
                healthBar.value = (float)health;
            }
            invincOver = 0;
            invincible = true;
            dmgAnim.SetBool("Invincible", invincible);
            Physics.IgnoreLayerCollision(6, 7, true);

            return true;
        }
        return false;
    }

    public override void constantDamage(double dps)
    {
        base.constantDamage(dps);
        if (healthBar)
        {
            healthBar.value = (float)health;
        }
    }

    public void acidAnim(bool active)
    {
        dmgAnim.SetBool("Acid", active);
    }

    public void heal(double hp)
    {
        health += hp;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        if (healthBar)
        {
            healthBar.value = (float)health;
        }
    }
}
