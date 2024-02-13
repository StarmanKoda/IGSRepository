using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLoader : MonoBehaviour
{
    public ZoneDoor[] zoneDoors;
    public GameObject[] rooms;
    public int curRoom = 0;

    public GameObject fade;
    Animator fadeAnim;

    [System.Serializable]
    public class Bounds
    {
        [SerializeField]
        public float maxX;
        public float minX;
        public float maxY;
        public float minY;
    }

    [SerializeField]
    public Bounds[] roomCamBounds;
    public CameraFollow cameraFol;
    float cameraSpeed = 0.125f;
    public float cameraJumpSpeed = 1f;

    public GameObject player;
    public float ascendMultiplier = 1.5f;
    Movement move;
    Rigidbody rig;

    public float roomLoadDelay = 0.4f;
    public float fadeSpeed = 0.3f;

    direction enterDir;

    private void Awake()
    {
        for (int i = 0; i < rooms.Length; i++)
        {
            if (i == curRoom)
            {
                continue;
            }
            rooms[i].SetActive(false);
        }

        cameraFol.maxX = roomCamBounds[curRoom].maxX;
        cameraFol.minX = roomCamBounds[curRoom].minX;
        cameraFol.maxY = roomCamBounds[curRoom].maxY;
        cameraFol.minY = roomCamBounds[curRoom].minY;
    }

    // Start is called before the first frame update
    void Start()
    {
        move = player.GetComponent<Movement>();
        rig = player.GetComponent<Rigidbody>();
        fadeAnim = fade.GetComponent<Animator>();

        fade.SetActive(true);

        fadeAnim.speed = 1f / fadeSpeed;

        cameraSpeed = cameraFol.smoothSpeed;
    }

    public void initialStart(float del)
    {
        move.enabled = false;
        Invoke("setup", del + roomLoadDelay);
    }

    void setup()
    {
        move.enabled = true;
        fadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fadeOut()
    {
        fadeAnim.Play("FadeOut");
    }

    public void fadeIn()
    {
        fadeAnim.Play("FadeIn");
        cameraFol.smoothSpeed = cameraSpeed;
    }

    public void LoadRoom(int nextRoom, direction dir)
    {
        rooms[curRoom].SetActive(false);
        rooms[nextRoom].SetActive(true);
        curRoom = nextRoom;

        cameraFol.maxX = roomCamBounds[curRoom].maxX;
        cameraFol.minX = roomCamBounds[curRoom].minX;
        cameraFol.maxY = roomCamBounds[curRoom].maxY;
        cameraFol.minY = roomCamBounds[curRoom].minY;

        cameraFol.smoothSpeed = cameraJumpSpeed;

        enterDir = dir;

        Invoke("startRoom", roomLoadDelay);
    }

    public void startRoom()
    {
        player.SetActive(true);
        move.enabled = true;

        move.resetVelocity();
        if (enterDir == direction.UP)
        {
            move.getRig().AddForce(new Vector2(0f, move.jumpForce * ascendMultiplier));
        }


        fadeIn();
    }
}
