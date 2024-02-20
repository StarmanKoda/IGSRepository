using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FallingSpike : MonoBehaviour
{
    public LayerMask mask;
    public float checkDistance;
    public bool temporary = false;
    public float replaceTime;
    Rigidbody rig;
    Collider col;

    Respawns respawner;
    public int replaceNum = -1;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();


        Transform topParent = transform;
        while (topParent.parent != null)
        {
            topParent = topParent.parent;
        }

        respawner = topParent.GetComponent<Respawns>();
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
                if (respawner)
                {
                    if (replaceNum >= 0)
                    {
                        GameObject replacement = respawner.earlyRespawn(replaceTime, replaceNum);
                        replacement.GetComponent<FallingSpike>().replaceNum = replaceNum;
                    }
                    else
                    {

                        replaceNum = respawner.getObjNum(gameObject);
                        GameObject replacement = respawner.earlyRespawn(replaceTime, replaceNum);
                        replacement.GetComponent<FallingSpike>().replaceNum = replaceNum;
                    }
                }

                Destroy(gameObject, replaceTime);
            }
            Destroy(this, .1f);
        }
    }
}
