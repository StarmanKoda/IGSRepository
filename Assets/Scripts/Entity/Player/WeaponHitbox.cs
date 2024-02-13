using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitbox : MonoBehaviour
{
    public double dmg = 50;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EntityScript>() == null) return;
        if (other.tag.ToLower() == "player") return;
        EntityScript entity = other.gameObject.GetComponent<EntityScript>();
        entity.takeDamage(dmg);
    }
}
