using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponUpgrade : MonoBehaviour
{
    // Start is called before the first frame update
    public string getID()
    {
        return SceneManager.GetActiveScene().name + "x-" + this.transform.position.x + "y-" + this.transform.position.y + "z-" + this.transform.position.z + "";
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!(other.tag == "Player")) { return; }
        CollectibleList.getInstance().addObject(getID());
        //Run a check
        int size = CollectibleList.getInstance().getCollected();
        attemptUnlock(size, UpgradeEnum.SPEAR);
        attemptUnlock(size, UpgradeEnum.GUN);
        attemptUnlock(size, UpgradeEnum.BLASTER);

        Destroy(this.gameObject);
    }

    void attemptUnlock(int size, UpgradeEnum weapon)
    {
        if (size == UpgradeEnumMethods.getCollectibleUpgrade(weapon))
        {
            Debug.Log("Obtained " + weapon.ToString());
            UpgradeInventory.getInstance().unlockUpgrade(UpgradeEnumMethods.getUpgrade(weapon));
        }
    }

    public void Start()
    {
        if (CollectibleList.getInstance() == null) return;
        if (CollectibleList.getInstance().containsObject(this.getID()))
        {
            Destroy(this.gameObject);
        }
    }

}
