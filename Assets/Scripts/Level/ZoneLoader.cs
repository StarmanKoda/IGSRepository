using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZoneLoader : MonoBehaviour
{
    ZoneDoor[] zoneDoors;
    public RoomLoader roomLoader;
    Movement player;
    public static ZoneLoader zoneLoader;

    public float extraZoneLoadDelay = 0f;
    bool setLoad = false;
    Vector3 loadLocation;
    ZoneDoor entranceDoor;

    int doorTo = -1;
    int loadRoom = -1;

    Vector2 velocity;
    Vector3 offset;

    public float master_volume = 1;
    public float music_volume = 1;
    public float sfx_volume = 1;

    AudioSource musicSource;

    //public Upgrades[] obtainedUpgrades;
    public double health;

    bool dead = false;

    public void setMasterVolume(float volume)
    {
        master_volume = volume;
        if (musicSource != null)
        {
            musicSource.volume = master_volume * music_volume;
        }
    }
    public void setMusicVolume(float vol)
    {
        music_volume = vol;
        if (musicSource != null)
        {
            musicSource.volume = master_volume * music_volume;
        }
    }
    public void setSfxVolume(float vol)
    {
        sfx_volume = vol;
    }

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
        if (musicSource != null)
        {
            musicSource.volume = master_volume * music_volume;
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
        if(setLoad)
        {
            player.transform.position = loadLocation;
        }

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
        //player.GetComponent<UpgradeInventory>().obtainedUpgrades = obtainedUpgrades;
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

    public void setLoadLocation(Vector3 loc)
    {
        setLoad = true;
        loadLocation = loc;
    }
}
