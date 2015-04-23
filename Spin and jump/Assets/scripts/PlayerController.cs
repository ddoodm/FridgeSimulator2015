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
        slowedSpeed = 0.025f,
        correctionSpeed = 0.01f;
	
	/// <summary>
	/// position of the player before wallrunning.
	/// </summary>
	public Vector3 tempWallJump;

	/// <summary>
	/// Flag to determine what direction the player is in.
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
	
    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        Debug.DrawRay(this.transform.position, Vector3.down);
    }

	void LateUpdate()
	{
        // Parallel transform along the path (infinite force).
        if (!isInAir)
        {
            if (!gameController.isGameOver && !isSlowed)
                rigidbody.position += transform.forward * moveSpeed;
            else
                rigidbody.position += transform.forward * slowedSpeed;
        }

        // Add gravity force
        rigidbody.AddForce(Vector3.down * gravityForce);

        // Add jump force
        if (Input.GetButton("Jump") && !isInAir)
        {
            // Transform jump direction into the player's local space
            rigidbody.AddForce(transform.rotation * jumpForce);
            isInAir = true;

			if (wallJump) {
				tempWallJump.x = rigidbody.position.x + 2.0f;
				tempWallJump.y = rigidbody.position.y + 2.0f;
				tempWallJump.z = rigidbody.position.z + 2.0f;
				wallRunning = true;
				wallJump = false;
				Debug.Log ("Wallrunning");
			}



        }

		//allow wall running
		/*if (Input.GetButtonDown ("Jump") && wallJump) {
			tempWallJump.x = rigidbody.position.x + 2.0f;
			tempWallJump.y = rigidbody.position.y + 2.0f;
			tempWallJump.z = rigidbody.position.z + 2.0f;

			if (!isInAir){
				wallRunning = true;
			}
			wallRunning = true;
			wallJump = false;
			Debug.Log ("Wallrunning");

		} */

		//while wall running
		if (wallRunning) {
			rigidbody.position += transform.forward * moveSpeed;
			tempPos = rigidbody.position;
			tempPos.y = tempWallJump.y;

			//add flag for whether player is travelling along x or z axis

			tempPos.x = tempWallJump.x;


			rigidbody.position = tempPos;
		}


        // Tend towards the middle of the current platform
        GameObject currentPlatform = this.currentPlatform;
        if (currentPlatform != null && currentPlatform.name.StartsWith("PF_Platform_S_Path") && !isInAir)
        {
            Vector3 platformRight = currentPlatform.transform.right;
            platformRight = new Vector3(Mathf.Abs(platformRight.x), Mathf.Abs(platformRight.y), Mathf.Abs(platformRight.z));
            Vector3 playerPos = this.transform.position;
            Vector3 platformPos = currentPlatform.transform.position;
            Vector3 distance = Vector3.Scale(platformPos - playerPos, platformRight);
            this.rigidbody.position += distance * correctionSpeed * Time.deltaTime;
        }

		//rotates the player if it is on a spinning platform
        /*
		if (onPlatform) 
		{
			transform.Rotate (new Vector3 (0, 30, 0) * Time.deltaTime);
		}*/

        oldPosition = rigidbody.position;
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

            return null;
        }
    }
}
