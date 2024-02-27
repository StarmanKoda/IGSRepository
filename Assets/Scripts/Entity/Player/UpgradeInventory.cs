using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeInventory : MonoBehaviour
{
    public Upgrades[] obtainedUpgrades = new Upgrades[7];
    static UpgradeInventory instance;
    public GameObject spearObj;
    public GameObject BulletObj;
    public bool attacking = false;
    void Start()
    {
        //Load Save Data

        //Fill unlocked upgrade list

        if(instance == null)
        {
            instance = this;
        }
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

    public static UpgradeInventory getInstance()
    {
        return instance;
    }

    public GameObject ShootBullet()
    {
        return Instantiate(BulletObj, this.gameObject.transform.position, Quaternion.identity);
    }

    public string[] toStringList()
    {
        List<string> s = new List<string>();
        foreach(Upgrades u in obtainedUpgrades)
        {
            if (u == null) continue;
            s.Add(u.getId().ToString());
        }
        return s.ToArray();
    }

    public void fromStringList(string[] s) { 
        for(int i = 0;i < s.Length;i++)
        {
            UpgradeEnum e = UpgradeEnumMethods.fromString(s[i]);
            unlockUpgrade(UpgradeEnumMethods.getUpgrade(e));
        }
    }
}
