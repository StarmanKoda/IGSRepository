using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropThrough : MonoBehaviour
{
    public bool startDrop = true;
    public bool dropping = true;

    private void OnEnable()
    {
        if (startDrop)
        {
            dropping = true;
            gameObject.layer = 9;
            Invoke("enableCol", 1);
        }
    }

    void enableCol()
    {
        if (dropping)
        {
            gameObject.layer = 3;
        }
    }

    private void OnCollisionStay(Collision collision)
    {

        if (Input.GetAxisRaw("Vertical") < 0)
        {
            gameObject.layer = 9;
            dropping = true;
        }
    }
}
