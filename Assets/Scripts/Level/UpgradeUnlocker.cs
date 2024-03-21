using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeUnlocker : MonoBehaviour
{
    public UpgradeEnum Upg;
    private void OnTriggerEnter(Collider player)
    {
        //Check if the object has an upgrade connected and if the collider can have an upgrade
        if (player.GetComponent<UpgradeInventory>() == null) return;
        //Unlock the upgrade (Or at least try)
        UpgradeInventory inv = player.GetComponent<UpgradeInventory>();
        //Determine which upgrade to give
        inv.unlockUpgrade(UpgradeEnumMethods.getUpgrade(Upg));
        SoundManager.Instance.blist[3] = true;
        Destroy(this.gameObject);
    }
}
