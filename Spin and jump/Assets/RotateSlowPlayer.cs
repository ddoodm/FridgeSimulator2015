using UnityEngine;
using System.Collections;

public class RotateSlowPlayer : MonoBehaviour {

	private PlayerController player;

	//if on the platform rotate and slow the player
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			player = other.gameObject.GetComponent<PlayerController>();
			player.isSlowed = true;
			//player.onPlatform = true;
		}
	}
	
	//player is off the platform
	void OnTriggerExit(Collider other) 
	{
		if (other.tag == "Player") {
			player.isSlowed = false;
			//player.onPlatform = false;
		}
	}
}
