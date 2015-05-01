using UnityEngine;
using System.Collections;
using System.Linq;

public class BiasPosition : MonoBehaviour
{
    public int
        lanes = 3;

    public float
        correctionSpeed = 0.01f,
        pathWidth = 5.0f;

    private int currentLane;

    private PlayerController playerController;

    private float laneOffset;

    void Start()
    {
        playerController = GetComponent<PlayerController>();

        resetCurrentLane();
    }

    /// <summary>
    /// Set current lane to the middle lane.
    /// </summary>
    public void resetCurrentLane()
    {
        currentLane = lanes / 2 + 1;
        laneOffset = laneToOffset(currentLane);
    }

    private float laneToOffset(int lane)
    {
        // Zero-number!
        lane--;

        float laneWidth = pathWidth / (float)lanes;

        // Map 0...x to -x/2...x/2
        lane -= lanes / 2;

        return laneWidth * (float)lane;
    }

	void LateUpdate ()
    {
        // We're alowed to push if we're in at least one push zone (path)
        playerController.pushAllowed = playerController.overlappingPushZones > 0;

        // If we're not in a push zone, reset the current lane
        if (!playerController.pushAllowed)
            resetCurrentLane();

        updateLane();

        // Tend towards the middle of the current platform, offset by the avoidance amount
        GameObject currentPlatform = playerController.currentPlatform;
        if (currentPlatform != null && currentPlatform.name.StartsWith("PF_Platform_S_Path") && !playerController.isInAir)
        {
            Vector3 platformRight = currentPlatform.transform.right;
            Vector3 playerPos = this.transform.position;
            Vector3 platformPos = currentPlatform.transform.position + platformRight * laneOffset;

            platformRight = new Vector3(Mathf.Abs(platformRight.x), Mathf.Abs(platformRight.y), Mathf.Abs(platformRight.z));
            Vector3 distance = Vector3.Scale(platformPos - playerPos, platformRight);
            this.rigidbody.position += distance * correctionSpeed * Time.deltaTime;
        }
	}

    private bool lastFrameAKey = false, lastFrameDKey = false;
    private void updateLane()
    {
        if (!playerController.pushAllowed ||
            playerController.isInAir ||
            playerController.canTurn ||
            playerController.currentPlatform == null ||
            (playerController.alreadyPushedForID == playerController.currentPlatform.GetInstanceID()))
            return;

        if (!lastFrameAKey && Input.GetKey(KeyCode.A))
            pushByLanes(-1);
        if (!lastFrameDKey && Input.GetKey(KeyCode.D))
            pushByLanes(+1);

        lastFrameAKey = Input.GetKey(KeyCode.A); lastFrameDKey = Input.GetKey(KeyCode.D);
    }

    public void pushByLanes(int lanesToJump)
    {
        // Clamp from lane 1 to lane [lanes] by the weirdest means you ever did see:
        if (!Enumerable.Range(1, lanes).Contains(currentLane + lanesToJump))
            return;

        currentLane += lanesToJump;

        // Update offset
        laneOffset = laneToOffset(currentLane);

        //playerController.pushAllowed = false;
        //playerController.alreadyPushedForID = playerController.currentPlatform.GetInstanceID();
    }
}
