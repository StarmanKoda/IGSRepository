using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : MonoBehaviour
{
    private float startY;
    public float variation = 0.2f;
    public float speed = 0.025f;
    public bool direction = false;
    // Start is called before the first frame update
    void Start()
    {
        startY = this.transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float newDirection = direction ? transform.position.y + speed * Time.timeScale : transform.position.y - speed * Time.timeScale;
        if(newDirection >= startY + variation || newDirection <= startY-variation) { direction = !direction; return; }
        transform.position = (new Vector3(transform.position.x, newDirection, transform.position.z));
    }
}
