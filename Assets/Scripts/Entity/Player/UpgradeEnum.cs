using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//namespace UpgradeEnum
//{
    public enum UpgradeEnum
    {
        DASH, DBLJUMP, GLIDE, WALLCLIMB
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

                    break;
                case UpgradeEnum.WALLCLIMB:

                    break;
            }
            return val;
        }
    }
//}