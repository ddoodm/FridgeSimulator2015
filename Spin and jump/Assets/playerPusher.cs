using UnityEngine;
using System.Collections;

public class playerPusher : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Player")
            return;

        other.GetComponent<PlayerController>().pushAllowed = true;
        Debug.Log("PUSH ALLOWED for " + this.gameObject);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Player")
            return;

        other.GetComponent<PlayerController>().pushAllowed = false;
        Debug.Log("PUSH DISABLED for " + this.gameObject);
    }
}
