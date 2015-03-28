using UnityEngine;
using System.Collections;

public class JumpToPlatform : MonoBehaviour {

	private PlayerController player;


	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			player = other.gameObject.GetComponent<PlayerController>();
			if(player.isSlowed)
			{
				player.spinJump = true;
			}
		}
	}
	
	
	void OnTriggerExit(Collider other) 
	{
		if (other.tag == "Player") 
			if(!player.isSlowed)
		{
			player.spinJump = false;
		}
	}
}
