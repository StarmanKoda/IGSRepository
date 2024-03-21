using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public SaveState saveState;
    public GameObject displayBeam;
    public float beamTime = 1.5f;

    public void Start()
    {
        displayBeam.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        saveState.save();
        displayBeam.SetActive(true);
        Invoke("disableBeam", beamTime);
    }

    public void disableBeam()
    {
        displayBeam.SetActive(false);
    }
}
