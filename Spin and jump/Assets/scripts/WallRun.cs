using UnityEngine;
using System.Collections;

public class WallRun : MonoBehaviour {

	private PlayerController player;
	
	
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			player = other.gameObject.GetComponent<PlayerController>();
			player.wallJump = true;
			Debug.Log("Can jump onto wallrun");
		}
	}
	
	
	void OnTriggerExit(Collider other) 
	{
		if (other.tag == "Player") {
			Debug.Log ("Fallen from wallrun");
			player.wallRunning = false;
		}
	} 
}
