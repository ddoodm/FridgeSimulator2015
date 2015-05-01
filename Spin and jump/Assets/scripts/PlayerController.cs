using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
    /// <summary>
    /// Inspector parameters for jump force (height) and gravity force.
    /// </summary>
    public Vector3 jumpForce = new Vector3(0.0f, 300.0f, 75.0f);
	public Vector3 tempPos;
	public float gravityForce   = 15.0f;

    /// <summary>
    /// The amount of parallel transfer per frame.
    /// (Temp.)
    /// </summary>
    public float
        moveSpeed = 0.1f,
        slowedSpeed = 0.025f;
		
	float speed;
	
	/// <summary>
	/// position of the player before wallrunning.
	/// </summary>
	public Vector3 tempWallJump;

	/// <summary>
	/// Flag to determine what direction the player is in, will turn into an enum later if code is unreadable.
	/// </summary>
	public int direction;

    /// <summary>
    /// Flag used to constrain jumping to when the player is intersecting the floor.
    /// </summary>
    public bool isInAir { get; protected set; }
	/// <summary>
	/// Flag used to slow player before rotating platform.
	/// </summary>
    /// 
	public bool isSlowed { get; set; }
	/// <summary>
	/// Flag used to allow player to jump onto spinning platform.
	/// </summary>
	public bool spinJump { get; set; }

	/// <summary>
	/// Flags used to allow player to jump and run along wall platforms.
	/// </summary>
	public bool wallJump { get; set; }
	public bool wallRunning { get; set; }
    public GameObject wallRef = null;

    public bool canTurn { get; set; }

    /// <summary>
    /// The tag names for all existing platform types
    /// </summary>
    public string[] platformTags = new string[] {"Platform", "Spinner"};

    /// <summary>
    /// Access to Unity types
    /// </summary>
    public GameObject gameObject { get { return base.gameObject; } }
    public Transform transform { get { return base.transform; } }

    private GameController gameController;

    private Vector3 oldPosition;

    /// <summary>
    /// Amount of force to apply on the player's right axis.
    /// 750 = 12.5N * 1/60  :   12.5N force in the first frame.
    /// </summary>
    public float pushAmount = 750.0f;
    public bool pushAllowed = false;
    public int overlappingPushZones = 0;
    public int alreadyPushedForID = -1;

	public AudioSource jump, land, step, stepSlow;
	
    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

	void LateUpdate()
	{
        // Parallel transform along the path (infinite force).
        if (!isInAir) {
			if (!gameController.isGameOver && !isSlowed) 
				rigidbody.position += transform.forward * moveSpeed;
			else
				rigidbody.position += transform.forward * slowedSpeed;
		}

		if (isInAir && !isSlowed)
			step.Play ();

        // Add gravity force
        rigidbody.AddForce(Vector3.down * gravityForce);

        // Add jump force
        if (Input.GetButton("Jump") && !isInAir)
        {
            // Transform jump direction into the player's local space
            rigidbody.AddForce(transform.rotation * jumpForce);
            isInAir = true;
			jump.Play();
        }

        if (wallRunning && isInAir)
            handleWallJump();

        /* Player nudging
         * CONDITIONS:
         *  - Pushing must have been allowed by a 'pushAllowed' trigger
         *  - Must not be in air (jumping)
         *  - Must not be at a turn segment (new, fixes high velocity bug)
         *  - This platform was not nudged upon before
         */
        /*
        if (pushAllowed && !isInAir && !canTurn && currentPlatform != null && (alreadyPushedForID != currentPlatform.GetInstanceID()))
        {
            if (Input.GetKey(KeyCode.A))
                pushPlayer(-pushAmount);
            else if (Input.GetKey(KeyCode.D))
                pushPlayer(pushAmount);
        }*/

        oldPosition = rigidbody.position;
	}

    private void handleWallJump()
    {
        if (wallRef == null)
            return;

        // Tend towards the wall
        Vector3 wallRight = wallRef.transform.parent.right;
        wallRight = new Vector3(Mathf.Abs(wallRight.x), Mathf.Abs(wallRight.y), Mathf.Abs(wallRight.z));
        Vector3 playerPos = this.transform.position;
        Vector3 wallPos = wallRef.transform.position;
        Vector3 distance = Vector3.Scale(wallPos - playerPos, wallRight);
        this.rigidbody.position += distance * 5.0f * Time.deltaTime;

        rigidbody.AddForce(transform.forward * 15.0f);
        rigidbody.AddForce(Vector3.up * 10.0f);
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

    public void slowTo(float slowedSpeed)
    {
        this.slowedSpeed = slowedSpeed;
        this.isSlowed = true;
    }

    public void stopSlow()
    {
        this.isSlowed = false;
    }

    public float velocityMagnitude
    {
        get
        {
            if (isInAir)
            {
                // Do not count 'up' velocity when jumping and falling
                Vector3 yLock = new Vector3(1.0f, 0.0f, 1.0f);
                return Vector3.Scale(rigidbody.velocity, yLock).magnitude * Time.deltaTime;
            }
            if (isSlowed)
                return slowedSpeed;
            return moveSpeed;
        }
    }

    public bool onPlatform
    {
        get
        {
            return currentPlatform != null;
        }
    }

    public GameObject currentPlatform
    {
        get
        {
            // Cast a ray down to determine whether we're over a platform
            RaycastHit hit;
            if (!Physics.Raycast(new Ray(transform.position, Vector3.down), out hit))
                return null;

            if (hit.collider.tag == "Platform")
                return hit.collider.gameObject;

            return null;
        }
    }

    public GameObject objectUnderPlayer
    {
        get
        {
            // Cast a ray down to determine whether we're over a platform
            RaycastHit hit;
            if (!Physics.Raycast(new Ray(transform.position, Vector3.down), out hit))
                return null;

            /* == Filter based on tag ==
            foreach (string tagName in platformTags)
                if (hit.collider.gameObject.tag == tagName)
                    return hit.collider.gameObject;
             */
            return hit.collider.gameObject;

            // return null;
        }
    }
}
