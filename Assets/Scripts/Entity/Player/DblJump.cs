using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DblJump : Upgrades
{
    Rigidbody body;
    float jump = 20f;
    bool doubleJump = false;
    UpgradeEnum Upgrades.getId()
    {
        return UpgradeEnum.GLIDE;
    }

    void Upgrades.upgradeUpdate(GameObject obj, UpgradeInventory inv)
    {
        bool huggingWall = false;
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
        if(Movement.getinstance() == null) { return; }
        if(Movement.getinstance().refreshdblJump) { Movement.getinstance().refreshdblJump = false; doubleJump = true; }
        //Reset Double Jump if on ground. No other checks needed since you have to jump before double jump. Can't double jump if grounded
        if (Movement.getinstance().grounded) { doubleJump = true; return; }
        //Cannot double jump? No need to continue.
        if (!doubleJump) { return; }
        if (Movement.getinstance().getJumping()) { return; }

        if (inv.getUnlockedUpgrade(UpgradeEnum.WALLCLIMB))
        {
            //Disable double jumping if hugging a wall
            if (Physics.CheckSphere(Movement.getinstance().wallCheck.position, 0.5f, Movement.getinstance().groundMask))
            {
                huggingWall = true;
            }
        }
        //Player is not on ground and has finished jumping. We can now double jump
        if (Input.GetButtonDown("Jump") && !huggingWall)
        {
            body.velocity = Vector3.zero;
            body.velocity += new Vector3(0, jump, 0);
            doubleJump = false;
        }
    }
}
