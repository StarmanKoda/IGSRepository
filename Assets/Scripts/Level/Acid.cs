using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : MonoBehaviour
{
    public double dps;
    public float slowMod;

    private void OnTriggerStay(Collider other)
    {
        EntityScript entity = other.GetComponent<EntityScript>();
        if (entity)
        {
            entity.constantDamage(dps);
            Rigidbody rig = other.GetComponent<Rigidbody>();
            if (rig)
            {
                rig.velocity = Vector3.ClampMagnitude(rig.velocity, 10);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Movement move = other.GetComponent<Movement>();
        if (move)
        {
            move.speed *= slowMod;
            move.airSpd *= slowMod;
        }
        PlayerHealth player = other.GetComponent<PlayerHealth>();
        if (player)
        {
            player.acidAnim(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Movement move = other.GetComponent<Movement>();
        if (move)
        {
            move.speed /= slowMod;
            move.airSpd /= slowMod;
        }
        PlayerHealth player = other.GetComponent<PlayerHealth>();
        if (player)
        {
            player.acidAnim(false);
        }
    }
}
