using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDrop : MonoBehaviour
{
    public double amount;

    public float startForce;
    public int degreeRange;
    Rigidbody rig;

    public static int[] dropAmounts = { 5, 10, 50 }; //{ 1, 5, 10, 50 };
    public static float[] dropSizes = { .4f, .5f, .7f, 1f }; //{ .1f, .2f, .3f, .7f, 1f };

    private void OnDisable()
    {
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
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
            SoundManager.Instance.blist[9] = true;
            Destroy(this.gameObject, .1f);
        }
    }
}
