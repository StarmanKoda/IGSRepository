using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Door : MonoBehaviour
{
    public RoomLoader roomLoader;

    public int roomTo;

    public direction exitDir;

    bool locked = true;
    bool inDoorWay = false;

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
        Invoke("Open", roomLoader.roomLoadDelay + roomLoader.fadeSpeed);
    }

    void Open()
    {
        if (!inDoorWay && gameObject.activeInHierarchy)
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
            inDoorWay = false;

            if (exitDir == direction.DOWN)
            {
                if (other.transform.position.y < transform.position.y)
                {
                    move.enabled = false;
                    move.gameObject.SetActive(false);
                    Invoke("nextRoom", roomLoader.fadeSpeed);
                    roomLoader.fadeOut();
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Movement move = other.GetComponent<Movement>();
        if (move)
        {
            if ((exitDir == direction.RIGHT && (move.getMove() == 1 || Input.GetAxis("Dash") == 1)) || 
                (exitDir == direction.LEFT && (move.getMove() == -1 || Input.GetAxis("Dash") == -1)))
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
