using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Upgrades
{
    UpgradeEnum getId();

    void upgradeUpdate(GameObject obj, UpgradeInventory inv);
}
