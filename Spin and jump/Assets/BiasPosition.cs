using UnityEngine;
using System.Collections;

public class BiasPosition : MonoBehaviour
{
    public float
        correctionSpeed = 0.01f;

    private PlayerController playerController;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

	void LateUpdate ()
    {
        // Tend towards the middle of the current platform, offset by the avoidance amount
        GameObject currentPlatform = playerController.currentPlatform;
        if (currentPlatform != null && currentPlatform.name.StartsWith("PF_Platform_S_Path") && !playerController.isInAir)
        {
            Vector3 platformRight = currentPlatform.transform.right;
            platformRight = new Vector3(Mathf.Abs(platformRight.x), Mathf.Abs(platformRight.y), Mathf.Abs(platformRight.z));
            Vector3 playerPos = this.transform.position;
            Vector3 platformPos = currentPlatform.transform.position;
            Vector3 distance = Vector3.Scale(platformPos - playerPos, platformRight);
            this.rigidbody.position += distance * correctionSpeed * Time.deltaTime;
        }
	}
}
