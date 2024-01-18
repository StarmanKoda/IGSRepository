using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Door : MonoBehaviour
{


    public RoomLoader roomLoader;
    public CameraFollow cameraFol;
    public int roomTo;

    public float maxX;
    public float minX;
    public float maxY;
    public float minY;

    public static float playerEnterTime = 1f;
    public static float roomLoadDelay = 1f;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        //FADE IN
        Movement.getinstance().enabled = false;
        Invoke("startRoom", playerEnterTime);
    }

    void startRoom()
    {
        Movement.getinstance().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Movement move = other.GetComponent<Movement>();
        if (move)
        {
            player = move.gameObject;
            player.SetActive(false);
            Invoke("nextRoom", roomLoadDelay); 
            //FADE OUT
        }
    }

    void nextRoom()
    {
        cameraFol.maxX = maxX;
        cameraFol.minX = minX;
        cameraFol.maxY = maxY;
        cameraFol.minY = minY;
        player.SetActive(true);
        roomLoader.LoadRoom(roomTo);
    }
}
