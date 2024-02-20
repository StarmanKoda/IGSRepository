using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : Upgrades
{
    Melee melee = null;
    public float maxChargeSize;

    float nextAttack = 0f;
    bool canAtk = false;

    public UpgradeEnum getId()
    {
        return UpgradeEnum.GUN;
    }

    public void upgradeUpdate(GameObject obj, UpgradeInventory inv)
    {
        if (melee == null)
        {
            melee = obj.GetComponent<Melee>();
        }
        if(nextAttack > 0f)
        {
            nextAttack -= Time.deltaTime;
            if (nextAttack <= 0)
            {
                melee.noPogo = false;
                melee.enabled = true;
                nextAttack = 0;
                inv.attacking = false;
            }
            if (nextAttack <= attkRate && canAtk)
            {
                inv.ShootBullet();
                canAtk = false;
            }
        }

        if(!inv.attacking &&  melee.nextAttack <= 0)
        {
            if (Input.GetButtonDown("Attack3")) //"Attack1"
            {
                canAtk = true;
                nextAttack = attkDel + attkRate;
                melee.noPogo = true;
                melee.enabled = false;
                inv.attacking = true;
            }
        }

    }
}
