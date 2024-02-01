using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitFall : MonoBehaviour
{
    public double pitDmg;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Movement move = other.GetComponent<Movement>();
        if (move)
        {
            other.transform.position = move.getLastGrounded();
            move.resetVelocity();
            other.GetComponent<EntityScript>().takeDamage(pitDmg);
        }
    }
}