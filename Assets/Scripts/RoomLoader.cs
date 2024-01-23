using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RoomLoader : MonoBehaviour
{
    public GameObject[] rooms;
    public int startingRoom;
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
    public float enterSpeed = 5f;
    Movement move;
    Rigidbody rig;

    int curRoom = 0;

    public float roomLoadDelay = 0.4f;
    public float fadeSpeed = 0.3f;

    private void Awake()
    {
        for (int i = 0; i < rooms.Length; i++)
        {
            if (i == startingRoom)
            {
                continue;
            }
            rooms[i].SetActive(false);
        }
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fadeOut()
    {
        fadeAnim.Play("FadeOut");
        cameraFol.smoothSpeed = cameraJumpSpeed;
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

        Invoke("startRoom", roomLoadDelay);
    }

    public void startRoom()
    {
        player.SetActive(true);
        move.enabled = true;
        move.resetVelocity();

        fadeIn();
    }
}
