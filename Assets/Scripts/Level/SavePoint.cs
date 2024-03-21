using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public SaveState saveState;
    public GameObject displayBeam;
    public float beamTime = 1.5f;
    bool active = false;

    public void Start()
    {
        displayBeam.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(active) { return; }
        if (!other.gameObject.tag.ToLower().Equals("player")) { return; }
        if(other.gameObject.GetComponent<PlayerHealth>() == null) { return;  }
        active = true;
        PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();
        saveState.save();
        displayBeam.SetActive(true);
        Invoke("disableBeam", beamTime);
        player.heal(player.maxHealth);
    }

    public void disableBeam()
    {
        displayBeam.SetActive(false);
        active = false;
    }
}
