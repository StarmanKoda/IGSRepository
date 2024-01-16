using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeInventory : MonoBehaviour
{
    public Upgrades[] obtainedUpgrades = new Upgrades[sizeof(UpgradeEnum)];
    void Start()
    {
        //Load Save Data

        //Fill unlocked upgrade list

    }

    private void Update()
    {
        foreach (Upgrades upgrade in obtainedUpgrades) {
            if (upgrade == null) continue;
            upgrade.upgradeUpdate(this.gameObject);
        }
    }

    public void unlockUpgrade(Upgrades upgrade)
    {
        for(int i = 0; i < obtainedUpgrades.Length; i++)
        {
            if (obtainedUpgrades[i] == null)
            {
                obtainedUpgrades[i] = upgrade;
                return;
            }
            if (obtainedUpgrades[i].getId() == upgrade.getId()) return;
        }
        
    }
}
