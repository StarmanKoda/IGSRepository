using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawns : MonoBehaviour
{
    public GameObject[] objectsToRespawn;
    GameObject[] copies;

    public void Awake()
    {
        foreach (GameObject obj in objectsToRespawn)
        {
            obj.SetActive(false);
        }

        copies = new GameObject[objectsToRespawn.Length];
    }

    private void OnEnable()
    {
        for (int i = 0; i < copies.Length; i++)
        {
            GameObject newObj = Instantiate(objectsToRespawn[i]);
            newObj.transform.position = objectsToRespawn[i].transform.position;
            newObj.transform.rotation = objectsToRespawn[i].transform.rotation;
            newObj.SetActive(true);
            copies[i] = newObj;
        }
    }

    private void OnDisable()
    {
        foreach (GameObject obj in copies)
        {
            Destroy(obj);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
