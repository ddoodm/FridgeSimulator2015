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
    /// The direction of parallel transfer per frame.
    /// (Temp.)
    /// </summary>
    public Vector3 pathDirection = Vector3.forward;

    /// <summary>
    /// The amount of parallel transfer per frame.
    /// (Temp.)
    /// </summary>
    public float 
		moveSpeed = 0.1f, 
		slowedSpeed = 0.25f;

    /// <summary>
    /// The Y value at which the player will be killed (game-over).
    /// </summary>
    public float deathHeight = -1.0f;

    /// <summary>
    /// Flag used to constrain jumping to when the player is intersecting the floor.
    /// </summary>
    public bool isInAir { get; protected set; }
	/// <summary>
	/// Flag used to slow player before rotating platform.
	/// </summary>
	public bool isSlowed { get; set; }

    private GameController gameController;

    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        // Check for player intersection with the "death plane".
        if (transform.position.y <= deathHeight)
            gameController.GameOver();
    }

	void LateUpdate()
	{
        // Parallel transform along the path (infinite force).
        if(!gameController.isGameOver && !isSlowed)
            rigidbody.position += pathDirection * moveSpeed;
		else
			rigidbody.position += pathDirection * slowedSpeed;

        // Add gravity force
        rigidbody.AddForce(Vector3.down * gravityForce);

        // Add jump force
        if (Input.GetButton("Jump") && !isInAir)
        {
            rigidbody.AddForce(jumpForce);
            isInAir = true;
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
