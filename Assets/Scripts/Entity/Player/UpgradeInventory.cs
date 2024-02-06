using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeInventory : MonoBehaviour
{
    public Upgrades[] obtainedUpgrades = new Upgrades[sizeof(UpgradeEnum)];
    static UpgradeInventory instance;
    void Start()
    {
        //Load Save Data

        //Fill unlocked upgrade list

    }

    private void Update()
    {
        if (Time.timeScale == 0) return;
        foreach (Upgrades upgrade in obtainedUpgrades) {
            if (upgrade == null) continue;
            upgrade.upgradeUpdate(this.gameObject, this);
        }
    }

    public void unlockUpgrade(Upgrades upgrade)
    {
        if (getUnlockedUpgrade(upgrade.getId())) { return; }
        if(UpgradeEnumMethods.getId(upgrade.getId()) == -1) { return; }
        obtainedUpgrades[UpgradeEnumMethods.getId(upgrade.getId())] = upgrade;
        
    }

    public bool getUnlockedUpgrade(UpgradeEnum upgrade)
    {
        for(int i = 0; i < obtainedUpgrades.Length; i++) {
            if (obtainedUpgrades[i] == null) continue;
            if (obtainedUpgrades[i].getId().Equals(upgrade)) return true;
        }
        return false;
    }
}
