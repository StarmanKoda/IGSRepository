using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityScale : MonoBehaviour
{
    public float gScale;
    Rigidbody rig;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }



    // Update is called once per frame
    void Update()
    {
        //print(rig.velocity.y + " " + gScale);
    }
    void FixedUpdate()
    {
        Vector3 gravity = -9.81f * gScale * Vector3.up;
        rig.AddForce(gravity, ForceMode.Acceleration);
    }
}
