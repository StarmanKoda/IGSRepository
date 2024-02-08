using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float timeToFall;
    public float shakeSpeed;
    public float shakeDistance;
    public float fallMod;
    float maxShake;
    float shake;
    bool incr = true;
    bool impacted = false;
    bool falling = false;

    Rigidbody rig;

    // Start is called before the first frame update
    void Start()
    {
        maxShake = 1 / shakeSpeed;
        shake = maxShake / 2;

        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (falling)
        {
            rig.position = rig.position + Vector3.down * 9.81f * Time.deltaTime * fallMod;
        }
        if (impacted && !falling)
        {
            if (incr)
            {
                shake += Time.deltaTime;
                if (shake > maxShake)
                {
                    shake = maxShake;
                    incr = false;
                }
            }
            else
            {
                shake -= Time.deltaTime;
                if (shake < 0)
                {
                    shake = 0;
                    incr = true;
                }
            }
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Lerp(-shakeDistance, shakeDistance, shake/maxShake));
            timeToFall -= Time.deltaTime;
            if (timeToFall < 0)
            {
                falling = true;
                //rig.useGravity = true;
                //rig.isKinematic = false;
                Destroy(gameObject, 10);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Movement move = collision.gameObject.GetComponent<Movement>();
        if (!impacted)
        {
            if (move)
            {
                Vector3 point = collision.contacts[0].normal;
                if (point.y < 0)
                {
                    impacted = true;
                }
            }
        }
        else if (falling)
        {
            Vector3 point = collision.contacts[0].normal;
            if (point.y > 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
