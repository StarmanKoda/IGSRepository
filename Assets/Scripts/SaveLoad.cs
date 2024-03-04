using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public sealed class saveProfile
{
    public string name;
    public int zoneIndex = 0;
    public int roomIndex = 0;
    public string[] collectibles;
    public string[] upgrades;

    private saveProfile() { }

    public saveProfile(string name, int zoneIndex, int roomIndex, string[] collectibles, string[] upgrades)
    {
        this.name = name;
        this.zoneIndex = zoneIndex;
        this.roomIndex = roomIndex;
        this.collectibles = collectibles;
        this.upgrades = upgrades;
    }
    public int getRoomIndex()
    {
        return roomIndex;
    }

    public String[] getCollectibles()
    {
        return collectibles;
    }

    public string[] getUpgrades()
    {
        return upgrades;
    }
}
