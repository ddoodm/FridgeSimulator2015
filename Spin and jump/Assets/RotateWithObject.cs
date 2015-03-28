using UnityEngine;
using System.Collections;

public class RotateWithObject : MonoBehaviour
{
    Rotator rotator;

    void Start()
    {
        // Obtain the rotation controller
        rotator = GetComponent<Rotator>();
    }

    void OnCollisionStay(Collision c)
    {
        if (c.gameObject.tag == "Player")
        {
            GameObject player = c.gameObject;
            player.transform.Rotate(rotator.axis, rotator.deltaRotation);
        }
    }
}
