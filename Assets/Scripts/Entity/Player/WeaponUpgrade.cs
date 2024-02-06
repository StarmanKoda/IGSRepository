using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgrade : MonoBehaviour
{
    // Start is called before the first frame update
    public int getID()
    {
        return this.GetInstanceID();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!(other.tag == "Player")) { return; }
        CollectibleList.getInstance().addObject(getID());
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
