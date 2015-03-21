using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
    /// <summary>
    /// Inspector parameters for jump force (height) and gravity force.
    /// </summary>
	public float
        jumpForce       = 250.0f,
        gravityForce    = 15.0f;

    /// <summary>
    /// The direction of parallel transfer per frame.
    /// (Temp.)
    /// </summary>
    public Vector3 pathDirection = Vector3.forward;

    /// <summary>
    /// The amount of parallel transfer per frame.
    /// (Temp.)
    /// </summary>
    public float moveSpeed = 0.1f;

    /// <summary>
    /// Flag used to constrain jumping to when the player is intersecting the floor.
    /// </summary>
    public bool isInAir { get; protected set; }

	void FixedUpdate()
	{
        // Parallel transform along the path (infinite force).
        rigidbody.position += pathDirection * moveSpeed;

        // Add gravity force
        rigidbody.AddForce(Vector3.down * gravityForce);

        // Add jump force
        if (Input.GetButton("Jump") && !isInAir)
            rigidbody.AddForce(Vector3.up * jumpForce);
	}

    void OnCollisionEnter(Collision other)
    {
        // TODO: Check whether any of the contact points' normals are upward-facing
        // before setting the 'isInAir' flag.
        isInAir = false;
    }

    void OnCollisionExit(Collision other)
    {
        isInAir = true;
    }
}
