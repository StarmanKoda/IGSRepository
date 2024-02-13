using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDrop : MonoBehaviour
{
    public double amount;
    float basicAmount = 10;
    public float scaleBy = 0.05f;

    public float startForce;
    public int degreeRange;
    Rigidbody rig;

    public static int[] dropAmounts = { 0, 5, 10, 50 };
    public static float[] dropSizes = { .1f, .2f, .3f, .7f, 1f };

    private void OnDisable()
    {
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        //float scale = scaleBy * (float)(amount / basicAmount - 1);
        //transform.localScale += new Vector3(scale, scale, scale);

        rig = GetComponent<Rigidbody>();

        int degree = Random.Range(-1 * degreeRange / 2, degreeRange / 2);
        float radian = (float)degree * Mathf.PI / 180;
        rig.AddForce(new Vector2(startForce * Mathf.Sin(radian), startForce * Mathf.Cos(radian)));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();
        if (player)
        {
            player.heal(amount);
            Destroy(this.gameObject, .1f);
        }
    }
}
