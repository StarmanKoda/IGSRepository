using System.Collections;
using System.Collections.Generic;
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


        Destroy(this.gameObject);
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
