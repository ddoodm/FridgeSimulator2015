using UnityEngine;
using System.Collections;

public class playerPusher : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Player")
            return;

        PlayerController player = other.GetComponent<PlayerController>();

        // Do not allow pushing if the player can turn on a turn segment instead:
        if (player.canTurn)
        {
            player.pushAllowed = false;
            return;
        }

        player.pushAllowed = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Player")
            return;

        other.GetComponent<PlayerController>().pushAllowed = false;
        Debug.Log("PUSH DISABLED for " + this.gameObject);
    }
}
