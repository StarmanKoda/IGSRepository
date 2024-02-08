using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpike : MonoBehaviour
{
    public LayerMask mask;
    public float checkDistance;
    public bool temporary = false;
    public float timeToDestroy;
    Rigidbody rig;
    Collider col;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, checkDistance, mask))
        {
            rig.isKinematic = false;
            col.isTrigger = true;
            col.enabled = false;
            Invoke("reEnable", .1f);
        }
    }

    void reEnable()
    {
        col.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 0 || other.gameObject.layer == 3)
        {
            rig.isKinematic = true;
            col.isTrigger = false;
            transform.Rotate(0, 0, 180);
            if (temporary)
            {
                Destroy(gameObject, timeToDestroy);
            }
            Destroy(this);
        }
    }
}
