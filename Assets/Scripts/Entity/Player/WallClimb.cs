using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallClimb : Upgrades
{
    Rigidbody body;
    double wallJumpCooldown = 0.5;
    float jump = 20f;
    float horizontal = 50f;
    Collider[] lastWall;
    UpgradeEnum Upgrades.getId()
    {
        return UpgradeEnum.WALLCLIMB;
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
        //Null Checks
        if (Movement.getinstance() == null) { return; }
        if(wallJumpCooldown > 0)
        {
            wallJumpCooldown -= Time.deltaTime;
            if (wallJumpCooldown < 0) { wallJumpCooldown = 0; }
        }
        //Checks
        if(Movement.getinstance().wallCheck == null) { return; }
        if (Movement.getinstance().grounded)
        {
            if(lastWall != null) { lastWall = null; }
            
            return;
        }
        if (Movement.getinstance().getJumping()) return;
        if(wallJumpCooldown > 0) { return; }
        if(Physics.CheckSphere(Movement.getinstance().wallCheck.position, 0.5f, Movement.getinstance().groundMask))
        {
            Collider[] hit = Physics.OverlapSphere(Movement.getinstance().wallCheck.position, 0.5f, Movement.getinstance().groundMask);
            if (lastWall != null)
            {
                foreach (Collider col in hit)
                {
                    foreach (Collider og in lastWall)
                    {
                        if (col == og)
                        {
                            return;
                        }
                    }
                }
            }
            if (Input.GetButtonDown("Jump"))
            {
                Movement.getinstance().Flip();
                body.velocity = Vector3.zero;
                Vector3 movement;
                if (Movement.getinstance().facingRight)
                {
                    movement = new Vector3(horizontal, jump, 0);
                }
                else
                {
                    movement = new Vector3(-horizontal, jump, 0);
                }
                body.velocity += movement;
                Movement.getinstance().refreshdblJump = true;
                lastWall = hit;
            }
        }
    }
}
