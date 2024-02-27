using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public sealed class saveProfile<T> where T : SaveProfileData
{
    public string name;
    public T data;

    private saveProfile() { }

    public saveProfile(string name, T data)
    {
        this.name = name;
        this.data = data;
    }
}

public abstract record SaveProfileData { }

public record PlayerSaveData : SaveProfileData
{
    int roomIndex;
    String[] collectibles;
    private string[] upgrades;

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

    public PlayerSaveData(int roomIndex, string[] collectibles, string[] upgrades)
    {
        this.roomIndex = roomIndex;
        this.collectibles = collectibles;
        this.upgrades = upgrades;
    }
}
