using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneDoor : MonoBehaviour
{
    ZoneLoader zoneLoader;
    public RoomLoader roomLoader;

    public int doorNum;
    public int roomIn;

    public int doorTo;
    public string sceneTo;


    public direction entranceDir;

    bool locked = true;
    bool inDoorWay = false;

    Movement player;

    // Start is called before the first frame update
    void Start()
    {
        zoneLoader = ZoneLoader.zoneLoader;
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
            player = move;
            move.enabled = false;
            move.gameObject.SetActive(false);
            Invoke("nextZone", roomLoader.fadeSpeed);
            roomLoader.fadeOut();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Movement move = other.GetComponent<Movement>();
        if (move)
        {
            player = move;
            locked = false;
            inDoorWay = false;

            if (entranceDir == direction.UP)
            {
                if (other.transform.position.y < transform.position.y)
                {
                    move.enabled = false;
                    move.gameObject.SetActive(false);
                    Invoke("nextZone", roomLoader.fadeSpeed);
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
            player = move;
            if ((entranceDir == direction.LEFT && (move.getMove() == 1 || Input.GetAxis("Dash") == 1)) ||
                (entranceDir == direction.RIGHT && (move.getMove() == -1 || Input.GetAxis("Dash") == -1)))
            {
                move.enabled = false;
                move.gameObject.SetActive(false);
                Invoke("nextZone", roomLoader.fadeSpeed);
                roomLoader.fadeOut();
            }
        }
    }

    void nextZone()
    {
        Vector3 offset = Vector3.zero;
        if (entranceDir == direction.LEFT || entranceDir == direction.RIGHT)
        {
            offset.y = player.transform.position.y - transform.position.y;
        }
        else
        {
            offset.x = player.transform.position.x - transform.position.x;
        }
        zoneLoader.LoadZone(doorTo, player.getRig().velocity, offset, sceneTo);
    }
}
