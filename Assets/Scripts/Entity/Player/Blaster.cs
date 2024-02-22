using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : Upgrades
{
    Melee melee = null;
    public float maxChargeSize = 2.5f;
    public float chargeSpeed = 2f;
    float currentSize = 0f;
    float damageMult = 2f;
    bool canAtk = false;

    public UpgradeEnum getId()
    {
        return UpgradeEnum.BLASTER;
    }

    public void upgradeUpdate(GameObject obj, UpgradeInventory inv)
    {
        if (melee == null)
        {
            melee = obj.GetComponent<Melee>();
        }

        if(canAtk)
        {
            if (Input.GetButton("Attack3") && currentSize < maxChargeSize)
            {
                currentSize += Time.deltaTime * chargeSpeed;
                if(currentSize > maxChargeSize)
                {
                    currentSize = maxChargeSize;
                }
            }
            if (Input.GetButtonUp("Attack3"))
            {
                GameObject bullet = inv.ShootBullet();
                canAtk = false;
                melee.noPogo = false;
                melee.enabled = true;
                inv.attacking = false;
                Vector3 scale = new Vector3(currentSize + bullet.transform.localScale.x, currentSize + bullet.transform.localScale.y, currentSize + bullet.transform.localScale.z);
                bullet.transform.localScale = scale;
                PlayerProjectile shot = bullet.GetComponent<PlayerProjectile>();
                shot.atkDMG = Mathf.Floor(shot.atkDMG * ((currentSize * damageMult) +1));
            }
        }

        if(!inv.attacking &&  melee.nextAttack <= 0)
        {
            if (Input.GetButtonDown("Attack3")) //"Attack1"
            {
                canAtk = true;
                melee.noPogo = true;
                melee.enabled = false;
                inv.attacking = true;
                currentSize = 0f;
            }
        }

    }
}
