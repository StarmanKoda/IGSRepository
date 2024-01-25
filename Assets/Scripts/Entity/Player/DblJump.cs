using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DblJump : Upgrades
{
    Rigidbody body;
    bool doubleJump = false;
    UpgradeEnum Upgrades.getId()
    {
        return UpgradeEnum.DBLJUMP;
    }

    void Upgrades.upgradeUpdate(GameObject obj)
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
        if(Movement.getinstance() == null) { return; }
        //Reset Double Jump if on ground. No other checks needed since you have to jump before double jump. Can't double jump if grounded
        if (Movement.getinstance().grounded) { doubleJump = true; return; }
        //Cannot double jump? No need to continue.
        if (!doubleJump) { return; }
        if (!Movement.getinstance().getJumping()) { return; }
        //Player is not on ground and has finished jumping. We can now double jump
    }
}
