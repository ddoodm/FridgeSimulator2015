using UnityEngine;
using System.Collections;

public class StickyJump : MonoBehaviour {

	private PlayerController player;
	
	void OnTriggerStay(Collider other)
	{
		if (other.tag == "Player") {
			player = other.gameObject.GetComponent<PlayerController>();
			player.stickyJumping = true;
			player.stickyRef = gameObject;
		}
	}
	
	
	void OnTriggerExit(Collider other) 
	{
		if (other.tag == "Player") {
			player.stickyRef = null;
			Debug.Log ("Fallen from sticky wall");
			player.stickyJumping = false;
			this.transform.parent.GetComponent<PlatformDropper>().drop();
		}
	} 
}
