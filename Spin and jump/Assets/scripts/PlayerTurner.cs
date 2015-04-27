using UnityEngine;
using System.Collections;

public enum Direction
{
    NONE,
    LEFT,
    RIGHT
};

public class RotateState
{
    public Direction direction = Direction.NONE;
    public float startTime = -1.0f;

    public void rotate(Direction direction, float time)
    {
        this.direction = direction;
        this.startTime = time;
    }

    public void stop()
    {
        direction = Direction.NONE;
        startTime = -1.0f;
    }
}

public class PlayerTurner : MonoBehaviour
{
    /// <summary>
    /// The curve that describes how the player turns
    /// </summary>
    public AnimationCurve turnSpeed;
    public float correctionSpeed = 0.05f;
	
    private PlayerController playerController;
    private RotateState rotateState = new RotateState();

    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    void LateUpdate()
    {
        getRotationRequest();

        // TODO: Make this the player's real 'up' vector.
        Vector3 playerUp = Vector3.up;

        if (rotateState.direction != Direction.NONE)
        {
            // Get time since the turn was requested
            float timeDelta = Time.time - rotateState.startTime;

            // Get the curve's differential at this time
            float animDelta = turnSpeed.Evaluate(timeDelta + Time.deltaTime) - turnSpeed.Evaluate(timeDelta);

            // Configure direction
            if (rotateState.direction == Direction.LEFT)
                animDelta *= -1.0f;

            // Rotate by curve delta
            transform.Rotate(playerUp, animDelta);

            // Update the rotation state if we're done (90')
            if (turnSpeed.Evaluate(timeDelta) >= 90.0f)
            {
                rotateState.stop();

                // Clamp to 90' increments
                float rightAngle = Mathf.Round(transform.rotation.eulerAngles.y / 90.0f) * 90.0f;
                transform.Rotate(new Vector3(0.0f, rightAngle, 0.0f) - transform.rotation.eulerAngles);
            }
        }
        else if(playerController.onPlatform && !playerController.isInAir)
        {
            // If we're not rotating, tend towards the nearest 90'.
            float rightAngle = Mathf.Round(transform.rotation.eulerAngles.y / 90.0f) * 90.0f;
            float rightDelta = transform.rotation.eulerAngles.y - rightAngle;
            transform.Rotate(Vector3.up, -rightDelta * correctionSpeed);
        }
    }

    void getRotationRequest()
    {
        // Do not get a request if we're still turning
        if (rotateState.direction != Direction.NONE)
            return;

        // NEW: Do not turn while the player can still be pushed
        if (playerController.pushAllowed)
            return;

        if (playerController.isInAir)
            return;

        // Rotate only on a static platform
        if (!playerController.onPlatform)
            return;

        // Allow turns only in "TurnTrigger" boxes
        if (!playerController.canTurn)
            return;

        if (Input.GetKey(KeyCode.D))
        {
            rotateState.rotate(Direction.RIGHT, Time.time);
            playerController.canTurn = false;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rotateState.rotate(Direction.LEFT, Time.time);
            playerController.canTurn = false;
        }
    }
}
