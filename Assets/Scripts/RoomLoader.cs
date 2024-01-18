using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLoader : MonoBehaviour
{
    public GameObject[] rooms;

    int curRoom = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadRoom(int newRoom)
    {
        rooms[curRoom].SetActive(false);
        rooms[newRoom].SetActive(true);
        curRoom = newRoom;
    }
}
