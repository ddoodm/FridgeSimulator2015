using UnityEngine;
using System.Collections;

public class RotateWithObject : MonoBehaviour
{
    Rotator rotator;

    public int playerControlledSpeed;

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
            // Activate this if we want the player to be able to change direction of spinner
            /*
            if (Input.GetAxis("Horizontal") < 0)
            {
                switch (rotator.direction)
                {
                    case -1: rotator.speed = 100; break;
                    case 1: rotator.speed = -100; break;
                }
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                switch (rotator.direction)
                {
                    case -1: rotator.speed = -100; break;
                    case 1: rotator.speed = 100; break;
                }
            }
             */
            if (Input.GetAxis("Horizontal") < 0 && rotator.direction == -1)
            {
                rotator.speed = playerControlledSpeed;
            }
            else if (Input.GetAxis("Horizontal") > 0 && rotator.direction == 1)
            {
                rotator.speed = playerControlledSpeed;
            }
            else
                rotator.speed = 50;
            dist.Scale(new Vector3(1.0f, 0.0f, 1.0f));
            player.transform.RotateAround(rotator.transform.position, rotator.axis, rotator.deltaRotation);
        }
    }
}
