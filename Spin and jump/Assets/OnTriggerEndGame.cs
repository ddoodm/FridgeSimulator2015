using UnityEngine;
using System.Collections;

public class OnTriggerEndGame : MonoBehaviour {

    public GameController controller;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            controller.GameOver();
        }
    }
}
