using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//namespace UpgradeEnum
//{
    public enum UpgradeEnum
    {
        DASH = 0, DBLJUMP = 1, GLIDE = 2, WALLCLIMB = 3
    }

static class UpgradeEnumMethods
{
    public static Upgrades getUpgrade(this UpgradeEnum Upg)
    {
        Upgrades val = new Dash();
        switch (Upg)
        {
            case UpgradeEnum.DASH:
                val = new Dash();
                break;
            case UpgradeEnum.DBLJUMP:
                val = new DblJump();
                break;
            case UpgradeEnum.GLIDE:
                val = new Glide();
                break;
            case UpgradeEnum.WALLCLIMB:
                val = new WallClimb();
                break;
        }
        return val;
    }

    public static int getId(UpgradeEnum Upg)
    {
        switch (Upg)
        {
            case UpgradeEnum.DASH:
                return 0;
            case UpgradeEnum.DBLJUMP:
                return 1;
            case UpgradeEnum.GLIDE:
                return 2;
            case UpgradeEnum.WALLCLIMB:
                return 3;
            default: return -1;
        }
    }
}
//}