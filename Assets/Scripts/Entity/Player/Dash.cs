using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Upgrades
{
    public double dashCooldown = 1.5;
    double cooldown = 0;
    public static double speed = 50;
    double dashTime = 0;
    public static double dashTimeCooldown = 0.1;
    Vector3 dash = new Vector3(0f,0f, 0f);

    public UpgradeEnum getId()
    {
        return UpgradeEnum.DASH;
    }

    public void upgradeUpdate(GameObject obj)
    {

        if (dashTime > 0)
        {
            dashTime -= Time.deltaTime;
            Rigidbody body = obj.GetComponent<Rigidbody>();
            if (obj == null) return;
            body.velocity = dash * (Time.deltaTime * 500f);
            if (dashTime < 0) { dashTime = 0; }
            return;
        }

        if(cooldown > 0) { 
            cooldown -= Time.deltaTime; 
            if(cooldown <0) { cooldown = 0; }
        }



        //Check for button press
        if (Input.GetButtonDown("Dash"))
        {
            if (cooldown != 0) return;
            //Trigger Dash velocity in direction (1 = Left, -1 = Right
            Rigidbody body = obj.GetComponent<Rigidbody>();
            if (obj == null) return;
            dash = new Vector3((float)(speed * Input.GetAxis("Dash")), 0, 0);
            body.velocity = dash * (Time.deltaTime * 500f);
            cooldown = dashCooldown;
            dashTime = dashTimeCooldown;
        }

    }
}
