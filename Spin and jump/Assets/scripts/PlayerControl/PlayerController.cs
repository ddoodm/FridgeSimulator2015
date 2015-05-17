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
        moveSpeed,
        slowedSpeed = 0.025f,
        tempSpeed;
		
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
	public bool wallRunning { get; set; }
	public GameObject wallRef = null;

	/// <summary>
	/// Flags used to allow player to jump along sticky wall platforms.
	/// </summary>
	public bool stickyJumping { get; set; }
	private int numberOfJumps = -1;
	private Vector3 jumpForceRight = new Vector3(0.0f, 1.0f, 100.0f);
	public GameObject stickyRef = null;

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

    public CamShaker camera;
    private bool wasInAir;
	
    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    /// <summary>
    /// Handle rigidbody manipulation here
    /// </summary>
    void LateUpdate()
    {
        // @Rob, I don't know about this one just yet, it makes progress a little impossible even with new values.
        // jumpForce.z = moveSpeed * 2f;

        // Add gravity force
        rigidbody.AddForce(Vector3.down * gravityForce * Time.timeScale);

        // Check for turbo-jump bug, at fix it in the most sketchy way imaginable
        if (rigidbody.velocity.y > jumpForce.y)
            rigidbody.AddForce(Vector3.down * rigidbody.velocity.y * 0.5f, ForceMode.VelocityChange);

        if (!wasInAir && isInAir)
        {
            rigidbody.freezeRotation = false;
            rigidbody.AddForce(transform.forward * 20.0f, ForceMode.VelocityChange);
        }

        // Add jump force
        if (Input.GetButton("Jump") && !isInAir && !gameController.paused)
        {
            // Transform jump direction into the player's local space
            rigidbody.AddForce(transform.rotation * jumpForce, ForceMode.VelocityChange);
            isInAir = true;
        }

		//jumping for sticky platform
		if (Input.GetButton("Jump") && numberOfJumps != -1)
		{
			switch(numberOfJumps){
				case 0:
				case 2:
					rigidbody.AddForce(transform.rotation * -jumpForceRight);
					break;
				case 1:
				case 3:
					rigidbody.AddForce(transform.rotation * jumpForceRight);
					break;
				default :
					break;
			}
			// Transform jump direction into the player's local space
			numberOfJumps++;
		}

        if (wallRunning && isInAir)
            handleWallJump_Late();

		if (stickyJumping && isInAir)
			handleStickyJump_Late();

        wasInAir = isInAir;
    }

    /// <summary>
    /// Handle logic and transform manipulation / reading here
    /// </summary>
	void Update()
	{
        // Playtesters wanted free points for jumping obstacles
        if (objectUnderPlayer != null && objectUnderPlayer.tag == "Obstacle")
            gameController.AddScore(25.0f);

        if (objectUnderPlayer == null)
            isInAir = true;

        // Shake
        if (wasInAir && !isInAir)
            camera.shake();

        // Parallel transform along the path (infinite force).
        if (!isInAir)
            transform.position += velocity;

        if (wallRunning && isInAir)
            handleWallJump();

		if (stickyJumping && isInAir)
			handleStickyJump();

        oldPosition = transform.position;

        // Handle player speed when paused (Rob's stuff, slightly modified)
        if (moveSpeed > 0)
            tempSpeed = moveSpeed;
        moveSpeed = gameController.paused ? 0.0f : tempSpeed;
	}
	
    private void handleWallJump_Late()
    {
        // NEW: Hold jump to wallride
        if (!Input.GetButton("Jump"))
            return;

        // TODO: Give these inspector variables
        rigidbody.AddForce(transform.forward * 15.0f);
        rigidbody.AddForce(Vector3.up * 10.0f);
    }
	
    private void handleWallJump()
    {
        if (wallRef == null)
            return;

        // NEW: Hold jump to wallride
        if (!Input.GetButton("Jump"))
            return;

        // Tend towards the wall
        Vector3 wallRight = wallRef.transform.parent.right;
        wallRight = new Vector3(Mathf.Abs(wallRight.x), Mathf.Abs(wallRight.y), Mathf.Abs(wallRight.z));
        Vector3 playerPos = this.transform.position;
        Vector3 wallPos = wallRef.transform.position;
        Vector3 distance = Vector3.Scale(wallPos - playerPos, wallRight);
        this.rigidbody.position += distance * 5.0f * Time.deltaTime;
    }


	// Similar to Deinyons code but for sticky jumping.
	private void handleStickyJump_Late()
	{
		if (!Input.GetButton ("Jump")) {
			gravityForce = 15.0f;
			return;
		}
		gravityForce = 10.0f;
		//rigidbody.AddForce(Vector3.down * 0.1f);
		rigidbody.AddForce(Vector3.left * 5.0f);
		//rigidbody.AddForce(transform.forward * 15.0f);
		//rigidbody.AddForce(Vector3.up * 10.0f);
	}
	
	private void handleStickyJump()
	{
		if (stickyRef == null)
			return;
		
		// NEW: Hold jump to wallride
		if (!Input.GetButton ("Jump")) {
			Debug.Log("Plus: " + numberOfJumps);
			return;
		}

		if (numberOfJumps == -1)
			numberOfJumps = 0;

		if (numberOfJumps > 3) {
			numberOfJumps = -1;
			return;
		}

		// Tend towards the wall
		Vector3 stickRight = stickyRef.transform.parent.right;
		stickRight = new Vector3(Mathf.Abs(stickRight.x), Mathf.Abs(stickRight.y), Mathf.Abs(stickRight.z));
		Vector3 playerPos = this.transform.position;
		Vector3 wallPos = stickyRef.transform.position;

		if (numberOfJumps % 2 == 0) {
			Vector3 distance = Vector3.Scale (-wallPos - playerPos, stickRight);
			this.rigidbody.position += distance * 10.0f * Time.deltaTime;
			Debug.Log("Left: " + numberOfJumps);
		} else {
			Vector3 distance = Vector3.Scale (wallPos - playerPos, stickRight);
			this.rigidbody.position += distance * 10.0f * Time.deltaTime;
			Debug.Log("Right: " + numberOfJumps);
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

    public void slowTo(float slowedSpeed)
    {
        this.slowedSpeed = slowedSpeed;
        this.isSlowed = true;
    }

    public void stopSlow()
    {
        this.isSlowed = false;
    }

    public Vector3 velocity
    {
        get
        {
            if (!gameController.isGameOver && !isSlowed)
                return transform.forward * moveSpeed * Time.deltaTime;
            else
                return transform.forward * slowedSpeed * Time.deltaTime;
        }
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
            return velocity.magnitude;
        }
    }

    public bool onPlatform
    {
        get
        {
            return currentPlatform != null;
        }
    }

    public bool onObject
    {
        get
        {
            return objectUnderPlayer != null;
        }
    }

    public GameObject currentPlatform
    {
        get
        {
            GameObject obj = objectUnderPlayer;
            if (obj != null && obj.tag == "Platform")
                return obj;
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
            {
                // No hit on middle ray, try again:
                Ray rl = new Ray(transform.position - transform.right * 0.5f, Vector3.down);
                Ray rr = new Ray(transform.position + transform.right * 0.5f, Vector3.down);
                Ray rf = new Ray(transform.position + transform.forward * 0.5f, Vector3.down);
                Ray rb = new Ray(transform.position - transform.forward * 0.5f, Vector3.down);

                if (Physics.Raycast(rl, out hit))
                    return hit.collider.gameObject;
                if (Physics.Raycast(rr, out hit))
                    return hit.collider.gameObject;
                if (Physics.Raycast(rf, out hit))
                    return hit.collider.gameObject;
                if (Physics.Raycast(rb, out hit))
                    return hit.collider.gameObject;

                return null;
            }
            return hit.collider.gameObject;
        }
    }
}
