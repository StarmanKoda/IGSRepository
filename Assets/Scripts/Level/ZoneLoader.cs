using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZoneLoader : MonoBehaviour
{
    ZoneDoor[] zoneDoors;
    RoomLoader roomLoader;
    Movement player;
    public static ZoneLoader zoneLoader;

    public float extraZoneLoadDelay = 0.5f;

    ZoneDoor entranceDoor;

    int doorTo = -1;

    Vector2 velocity;
    Vector3 offset;

    public void Awake()
    {
        DontDestroyOnLoad(this);

        if (zoneLoader == null)
        {
            zoneLoader = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeZone();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadZone(int to, Vector3 vel, Vector3 off, string scene)
    {
        doorTo = to;
        velocity = vel;
        offset = off;

        SceneManager.LoadScene(scene);
    }

    void InitializeZone()
    {
        roomLoader = FindObjectOfType<RoomLoader>();
        player = Movement.getinstance();
        player.gameObject.SetActive(false);
        player.enabled = false;

        if (roomLoader == null)
        {
            zoneLoader = null;
            Destroy(this.gameObject);
            return;
        }
        
        if (doorTo >= 0)
        {
            entranceDoor = roomLoader.zoneDoors[doorTo];

            if (entranceDoor != null)
            {
                player.getRig().velocity = velocity;
                player.transform.position = entranceDoor.transform.position + offset;

                Invoke("LoadRoom", extraZoneLoadDelay);
            }

            doorTo = -1;
        }
        else
        {
            Invoke("InitialLoad", extraZoneLoadDelay);
        }

    }

    void LoadRoom()
    {
        roomLoader.LoadRoom(entranceDoor.roomIn, entranceDoor.entranceDir);
    }

    void InitialLoad()
    {
        roomLoader.LoadRoom(roomLoader.curRoom, direction.LEFT);
    }
}
