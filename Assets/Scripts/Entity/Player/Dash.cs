using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Upgrades
{
    public int dashCooldown = 3;
    double cooldown = 0;
    public static double speed = 3;
    public UpgradeEnum getId()
    {
        return UpgradeEnum.DASH;
    }

    public void upgradeUpdate(GameObject obj)
    {
        if(cooldown > 0) { 
            cooldown -= Time.deltaTime; 
            if(cooldown <0) { cooldown = 0; }
        }

        //Check for button press
        if (Input.GetButtonDown("Dash"))
        {
            if (cooldown != 0) return;
            //Trigger Dash velocity in direction (1 = Left, -1 = Right
            Rigidbody2D body = obj.GetComponent<Rigidbody2D>();
            if (obj == null) return;
            body.AddForce(new Vector2((float)(speed * -Input.GetAxis("Dash")), 0));
        }

    }
}
