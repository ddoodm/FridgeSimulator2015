using UnityEngine;
using System.Collections;

public class tutorialFinish : MonoBehaviour {

    public PlayerController player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider other) {
        if (other.CompareTag("Player"))
        {
            player.moveSpeed = 0;
        }
	
	}
}
