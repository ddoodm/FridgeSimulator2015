using UnityEngine;
using System.Collections;

public class SlowPlayer : MonoBehaviour
{
	private PlayerController player;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			player = other.gameObject.GetComponent<PlayerController>();
			player.isSlowed = true;
		}
	}

	
	void OnTriggerExit(Collider other) 
	{
		if (other.tag == "Player") 
			player.isSlowed = false;
	}
}
