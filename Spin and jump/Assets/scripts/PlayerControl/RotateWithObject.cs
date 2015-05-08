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

            Vector3 dist = rotator.transform.position - player.transform.position;
            dist.Scale(new Vector3(1.0f, 0.0f, 1.0f));
            player.transform.RotateAround(rotator.transform.position, rotator.axis, rotator.deltaRotation);
        }
    }
}
