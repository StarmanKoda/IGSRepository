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

    public float extraZoneLoadDelay = 0f;

    ZoneDoor entranceDoor;

    int doorTo = -1;
    int loadRoom = -1;

    Vector2 velocity;
    Vector3 offset;

    public Upgrades[] obtainedUpgrades;
    public double health;

    bool dead = false;

    public void Awake()
    {

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!dead)
        {
            InitializeZone();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);

        if (zoneLoader == null)
        {
            zoneLoader = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
            InitializeZone();
        }
        else
        {
            Destroy(this.gameObject);
            dead = true;
        }
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

        if (roomLoader == null)
        {
            //zoneLoader = null;
            //Destroy(this.gameObject);
            //dead = true;
            return;
        }

        player = Movement.getinstance();
        player.gameObject.SetActive(false);
        player.enabled = false;
        
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
            loadRoom = -1;
        }
        else
        {
            if (!dead)
            {
                Invoke("InitialLoad", extraZoneLoadDelay);
            }
        }
    }

    void LoadRoom()
    {
        player.GetComponent<UpgradeInventory>().obtainedUpgrades = obtainedUpgrades;
        player.GetComponent<EntityScript>().health = health;
        roomLoader.LoadRoom(entranceDoor.roomIn, entranceDoor.entranceDir);
    }

    void InitialLoad()
    {
        if (loadRoom >= 0)
        {
            roomLoader.LoadRoom(loadRoom, direction.LEFT);
        }
        else
        {
            roomLoader.LoadRoom(roomLoader.curRoom, direction.LEFT);
        }
    }

    public void setLoadRoom(int roomIndex)
    {
        loadRoom = roomIndex;
    }
}
