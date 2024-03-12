using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveState : MonoBehaviour
{

    public void load(string profileName)
    {
        saveProfile data = SaveManager.Load(profileName);
        if (data.getUpgrades() != null)
        {
            UpgradeInventory.getInstance().fromStringList(data.getUpgrades());
            //zoneLoader.obtainedUpgrades = UpgradeInventory.getInstance().obtainedUpgrades;
            foreach(Upgrades u in UpgradeInventory.obtainedUpgrades){
                Debug.Log("Obtained upgrades contains " + u);
            }
        }
        if (data.getCollectibles() != null)
        {
            CollectibleList.getInstance().fromStringArray(data.getCollectibles());
        }

        //Update ZoneLoader so that it knows proper room to load into
        ZoneLoader.zoneLoader.setLoadRoom(data.roomIndex);
        if (data.location != null)
        {
            ZoneLoader.zoneLoader.setLoadLocation(data.location);
        }
        //Reset timescale
        Time.timeScale = 1.0f;
        //Load Scene based on index
        SceneManager.LoadScene(data.zoneIndex);
    }
    
    public void save(string profileName)
    {

        saveProfile data = new saveProfile(profileName, SceneManager.GetActiveScene().buildIndex, ZoneLoader.zoneLoader.roomLoader.curRoom, Movement.getinstance().transform.position, CollectibleList.getInstance().toStringArray(), UpgradeInventory.getInstance().toStringList());
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
