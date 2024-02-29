using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveState : MonoBehaviour
{
    public GameObject Player;
    public UpgradeInventory inventory;
    public CollectibleList collectibleList;

    public void load(string profileName)
    {
        saveProfile data = SaveManager.Load(profileName);
        if (data.getUpgrades() != null)
        {
            inventory.fromStringList(data.getUpgrades());
        }
        if (data.getCollectibles() != null)
        {
            collectibleList.fromStringArray(data.getCollectibles());
        }
    }

    public void save(string profileName, int roomIndex)
    {

        saveProfile data = new saveProfile(profileName, roomIndex, collectibleList.toStringArray(), inventory.toStringList());
        foreach(string s in data.getUpgrades()) {
            Debug.Log(s);
        }
        foreach (string s in data.getCollectibles())
        {
            Debug.Log(s);
        }
        SaveManager.Save(data);
    }

    public void save(int roomIndex)
    {
        save("data0", roomIndex);
    }

    public void load()
    {
        load("data0");
    }
}
