using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Glide : Upgrades
{
    Rigidbody body;
    float fallSpeed = 0.2f;
    float glideCooldown = 0;
    public float glideCooldownTime = 0.2f;
    bool gliding = false;
    float maxSpeed = 10f;
    UpgradeEnum Upgrades.getId()
    {
        return UpgradeEnum.DBLJUMP;
    }

    void Upgrades.upgradeUpdate(GameObject obj, UpgradeInventory inv)
    {
        if (body == null)
        {
            body = obj.GetComponent<Rigidbody>();
            if (body == null)
            {
                Debug.Log("No rigid body.");
                return;
            }
        }
        if(glideCooldown > 0)
        {
            glideCooldown -= Time.deltaTime;
            if (glideCooldown < 0) { glideCooldown = 0; }
        }
        //Null Checks
        if (Movement.getinstance() == null) { return; }
        //If jumped and left grace period
        if (Movement.getinstance().grounded) { gliding = false; return; }
        
        if (Input.GetButton("Glide"))
        {
            gliding = true;
        }
        else
        {
            gliding = false;
        }
        if (Input.GetButtonUp("Glide")){
            glideCooldown = glideCooldownTime;
            gliding = false;
        }
        if (body.velocity.y >= 0) { gliding = false; }
        if (gliding && glideCooldown <= 0)
        {

            float passiveMove = 1f;
            if (!Movement.getinstance().facingRight)
            {
                passiveMove = -passiveMove;
            }
            //TODO: Change physics of glide to be more slippery like ice (Slow momentum to turn around)
            Vector3 newVel = new Vector3(Mathf.Clamp(body.velocity.x + passiveMove, -maxSpeed, maxSpeed), body.velocity.y * fallSpeed, body.velocity.z);
            
            body.velocity = newVel;
        }
    }
}
