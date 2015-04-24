using UnityEngine;
using System.Collections;

public class playerPusher : MonoBehaviour
{
    /// <summary>
    /// Amount of force to apply on the player's right axis.
    /// 750 = 12.5N * 1/60  :   12.5N force in the first frame.
    /// </summary>
    public float pushAmount = 750.0f;

    private PlayerController playerController;

    private bool pushAllowed = false;

    void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
            return;

        pushAllowed = true;
        Debug.Log("PUSH ALLOWED for " + this.gameObject);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Player")
            return;

        pushAllowed = false;
        Debug.Log("PUSH DISABLED for " + this.gameObject);
    }

    void FixedUpdate()
    {
        if (pushAllowed && !playerController.isInAir)
        {
            if (Input.GetKey(KeyCode.A))
                pushPlayer(-pushAmount);
            else if (Input.GetKey(KeyCode.D))
                pushPlayer(pushAmount);
        }
    }

    private void pushPlayer(float pushAmount)
    {
        playerController.rigidbody.velocity += (playerController.transform.right * pushAmount * Time.deltaTime);
        pushAllowed = false;
    }
}
