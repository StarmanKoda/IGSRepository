using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveState : MonoBehaviour
{
    GameObject Player;
    UpgradeInventory inventory;
    CollectibleList collectibleList;

    public void load(string profileName)
    {
        saveProfile<PlayerSaveData> profile = SaveManager.Load<PlayerSaveData>(profileName);
        PlayerSaveData data = profile.data;
        inventory.fromStringList(data.getUpgrades());
        collectibleList.fromStringArray(data.getCollectibles());
    }

    public void save(string profileName, int roomIndex)
    {

        PlayerSaveData data = new PlayerSaveData(roomIndex, collectibleList.toStringArray(), inventory.toStringList());
        saveProfile<PlayerSaveData> profile = new saveProfile<PlayerSaveData>(profileName, data);
        SaveManager.Save<PlayerSaveData>(profile);
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
