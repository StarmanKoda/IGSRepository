using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Upgrades
{
    public double dashCooldown = 3;
    double cooldown = 0;
    public static double speed = 100;
    double dashTime = 0;
    bool released = true;
    public static double dashTimeCooldown = 0.1;
    Vector3 dash = new Vector3(0f,0f, 0f);
    Rigidbody body;

    public UpgradeEnum getId()
    {
        return UpgradeEnum.DASH;
    }

    public void upgradeUpdate(GameObject obj)
    {
        if(body == null)
        {
            body = obj.GetComponent<Rigidbody>();
            if(body == null)
            {
                Debug.Log("No rigid body.");
                return;
            }
        }
        if (dashTime > 0)
        {
            dashTime -= Time.deltaTime;
            //obj.transform.position += dash * (Time.deltaTime);
            if (dashTime < 0) { dashTime = 0; }
            return;
        }

        if(cooldown > 0) { 
            
            int mult = 1;
            //if(movement.grounded) { groundSinceDash = true; }
            if(Movement.getinstance().grounded) { mult = 10; }
            cooldown -= Time.deltaTime * mult; 
            if(cooldown <0) { cooldown = 0; }
        }



        //Check for button press
        if ((Input.GetButtonDown("Dash") || Input.GetAxis("Dash") > 0.2 || Input.GetAxis("Dash") < -0.2) && released)
        {
            if (cooldown != 0) return;
            //Trigger Dash velocity in direction (1 = Left, -1 = Right
            dash = new Vector3((float)(speed * Input.GetAxis("Dash")), 0, 0);
            body.velocity += (dash);
            cooldown = dashCooldown;
            dashTime = dashTimeCooldown;
            released = false;
        }

        if(!(Input.GetButtonDown("Dash") || Input.GetAxis("Dash") > 0.2 || Input.GetAxis("Dash") < -0.2) && !released)
        {
            released = true;
        }
    }
}
