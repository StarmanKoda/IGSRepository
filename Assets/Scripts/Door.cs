using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Door : MonoBehaviour
{
    public RoomLoader roomLoader;

    public int roomTo;

    Collider col;

    public direction exitDir;

    bool locked = true;
    bool inDoorWay = false;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        Invoke("Open", roomLoader.roomLoadDelay + roomLoader.fadeSpeed);
    }

    void Open()
    {

        if (!inDoorWay)
        {
            locked = false;
        }
    }

    private void OnDisable()
    {
        locked = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (locked)
        {
            inDoorWay = true;
            return;
        }

        Movement move = other.GetComponent<Movement>();
        if (move)
        {
            move.enabled = false;
            move.gameObject.SetActive(false);
            Invoke("nextRoom", roomLoader.fadeSpeed);
            roomLoader.fadeOut();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Movement move = other.GetComponent<Movement>();
        if (move)
        {
            locked = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Movement move = other.GetComponent<Movement>();
        if (move)
        {
            if ((exitDir == direction.RIGHT && move.facingRight) || (exitDir == direction.LEFT && !move.facingRight))
            {
                move.enabled = false;
                move.gameObject.SetActive(false);
                Invoke("nextRoom", roomLoader.fadeSpeed);
                roomLoader.fadeOut();
            }
        }
    }

    void nextRoom()
    {
        roomLoader.LoadRoom(roomTo, exitDir);
    }
}
