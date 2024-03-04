using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveState : MonoBehaviour
{
    public GameObject Player;
    public UpgradeInventory inventory;
    public CollectibleList collectibleList;
    public ZoneLoader zoneLoader;

    public void load(string profileName)
    {
        saveProfile data = SaveManager.Load(profileName);
        if (data.getUpgrades() != null)
        {
            inventory.fromStringList(data.getUpgrades());
            zoneLoader.obtainedUpgrades = inventory.obtainedUpgrades;
        }
        if (data.getCollectibles() != null)
        {
            collectibleList.fromStringArray(data.getCollectibles());
        }

        //Update ZoneLoader so that it knows proper room to load into
        zoneLoader.setLoadRoom(data.roomIndex);
        //Reset timescale
        Time.timeScale = 1.0f;
        //Load Scene based on index
        SceneManager.LoadScene(data.zoneIndex);
    }

    public void save(string profileName)
    {

        saveProfile data = new saveProfile(profileName, SceneManager.GetActiveScene().buildIndex, zoneLoader.roomLoader.curRoom, collectibleList.toStringArray(), inventory.toStringList());
        SaveManager.Save(data);
    }

    public void save()
    {
        save("data0");
    }

    public void load()
    {
        load("data0");
    }
}
