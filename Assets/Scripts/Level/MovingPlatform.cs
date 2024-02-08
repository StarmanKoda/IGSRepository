using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform posA;
    public Transform posB;
    public bool AtoB = true;
    public float waitTime = 0f;
    float wait = 0f;
    public float speed = 50f;
    float distance;
    float traveled = 0f;

    Rigidbody rig;
    Vector3 lastPos;

    // Start is called before the first frame update
    void Start()
    {
        posA.gameObject.SetActive(false);
        posB.gameObject.SetActive(false);

        if (AtoB)
        {
            transform.position = posA.position;
        }
        else
        {
            transform.position = posB.position;
            traveled = distance;
        }

        distance = Mathf.Abs((posA.position - posB.position).magnitude);

        rig = GetComponent<Rigidbody>();

        wait = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        wait += Time.deltaTime;
        if (wait >= waitTime)
        {
            if (AtoB)
            {
                traveled += speed * Time.deltaTime;
                if (traveled > distance)
                {
                    traveled = distance;
                    AtoB = false;
                    wait = 0;
                }
            }
            else
            {
                traveled -= speed * Time.deltaTime;
                if (traveled < 0)
                {
                    traveled = 0;
                    AtoB = true;
                    wait = 0;
                }
            }
        }

        lastPos = rig.position;
        rig.position = Vector2.Lerp(posA.position, posB.position, traveled / distance);
    }

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb)
        {
            rb.position += rig.position - lastPos;
        }
    }
}
