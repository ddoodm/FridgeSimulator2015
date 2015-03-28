using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
    /// <summary>
    /// Inspector parameters for jump force (height) and gravity force.
    /// </summary>
    public Vector3 jumpForce = new Vector3(0.0f, 300.0f, 75.0f);
	public float gravityForce   = 15.0f;

    /// <summary>
    /// The amount of parallel transfer per frame.
    /// (Temp.)
    /// </summary>
    public float 
		moveSpeed = 0.1f, 
		slowedSpeed = 0.25f;

    /// <summary>
    /// Flag used to constrain jumping to when the player is intersecting the floor.
    /// </summary>
    public bool isInAir { get; protected set; }
	/// <summary>
	/// Flag used to slow player before rotating platform.
	/// </summary>
	public bool isSlowed { get; set; }
	/// <summary>
	/// Flag used to allow player to jump onto spinning platform.
	/// </summary>
	public bool spinJump { get; set; }
	/// <summary>
	/// Flag used to slow and rotate player on a rotating platform.
	/// </summary>
	public bool onPlatform { get; set; }

    private GameController gameController;

    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

	void LateUpdate()
	{
        // Parallel transform along the path (infinite force).
        if(!gameController.isGameOver && !isSlowed)
            rigidbody.position += transform.forward * moveSpeed;
		else
            rigidbody.position += transform.forward * slowedSpeed;

        // Add gravity force
        rigidbody.AddForce(Vector3.down * gravityForce);

        // Add jump force
        if (Input.GetButton("Jump") && !isInAir)
        {
            // Transform jump direction into the player's local space
            rigidbody.AddForce(transform.rotation * jumpForce);
            isInAir = true;
        }

		//rotates the player if it is on a spinning platform
		if (onPlatform) 
		{
			transform.Rotate (new Vector3 (0, 30, 0) * Time.deltaTime);
		}

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
}
