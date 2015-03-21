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

    private GameController gameController;

    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

	void FixedUpdate()
	{
        // Parallel transform along the path (infinite force).
        if(!gameController.isGameOver)
            rigidbody.position += pathDirection * moveSpeed;

        // Add gravity force
        rigidbody.AddForce(Vector3.down * gravityForce);

        // Add jump force
        if (Input.GetButton("Jump") && !isInAir)
            rigidbody.AddForce(Vector3.up * jumpForce);
	}

    void OnCollisionEnter(Collision other)
    {
        // Obtain distance from contact normal to ideal ground normal
        ContactPoint pContact = other.contacts[0];
        Vector3 contactNorm = pContact.normal;
        float angleDelta = Vector3.Distance(contactNorm, Vector3.up);

        // If the contact normal is (almost) up (flat surface), we are on the ground
        if (angleDelta < 0.25f)
            isInAir = false;
    }

    void OnCollisionExit(Collision other)
    {
        isInAir = true;
    }
}
