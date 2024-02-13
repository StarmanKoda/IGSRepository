using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDropThrough : MonoBehaviour
{
    DropThrough drop;

    private void Start()
    {
        drop = transform.parent.GetComponent<DropThrough>();
    }

    private void OnTriggerExit(Collider other)
    {
        drop.dropping = false;
        transform.parent.gameObject.layer = 3;
    }
}
