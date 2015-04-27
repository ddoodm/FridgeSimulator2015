using UnityEngine;
using System.Collections;

public class WallRun : MonoBehaviour {

	private PlayerController player;
	
	void OnTriggerStay(Collider other)
	{
		if (other.tag == "Player") {
			player = other.gameObject.GetComponent<PlayerController>();
			player.wallRunning = true;
            player.wallRef = gameObject;
		}
	}
	
	
	void OnTriggerExit(Collider other) 
	{
		if (other.tag == "Player") {
            player.wallRef = null;
			Debug.Log ("Fallen from wallrun");
			player.wallJump = false;
            player.wallRunning = false;
            this.transform.parent.GetComponent<PlatformDropper>().drop();
		}
	} 
}
