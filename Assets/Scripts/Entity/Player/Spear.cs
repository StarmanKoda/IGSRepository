using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spear : Upgrades
{

    Melee melee = null;

    public UpgradeEnum getId()
    {
        return UpgradeEnum.DASH;
    }

    public void upgradeUpdate(GameObject obj, UpgradeInventory inv)
    {
        if(melee == null)
        {
            melee = obj.GetComponent<Melee>();
            if(melee == null)
            {
                Debug.Log("No Melee.");
                return;
            }
        }

        
    }
}
