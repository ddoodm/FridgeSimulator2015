using UnityEngine;
using System.Collections;

public class playerPusher : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
            return;

        PlayerController player = other.GetComponent<PlayerController>();

        //player.pushAllowed = true;
        player.overlappingPushZones++;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Player")
            return;

        other.GetComponent<PlayerController>().overlappingPushZones--;

        Debug.Log("PUSH DISABLED for " + this.gameObject);
    }
}
