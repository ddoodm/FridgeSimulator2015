using UnityEngine;
using System.Collections;

public class TurnTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            other.GetComponent<PlayerController>().canTurn = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            other.GetComponent<PlayerController>().canTurn = false;
    }
}
