using UnityEngine;
using System.Collections;

public class progen : MonoBehaviour {

	public Transform spawner;
	public GameObject platformStraight;
	public GameObject platformRotating;
	public GameObject PlatformCorner;

	void OnTriggerEnter(Collider other)
	{
		int choice = Random.Range (0, 2);
		
		if (other.tag == "Player") {
		switch(choice)
			{
			case 0:
				Instantiate (platformStraight,spawner.position,spawner.rotation);
				break;
			case 1:
				Instantiate (platformRotating,spawner.position,spawner.rotation);
				break;

			default:
				Instantiate (platformStraight,spawner.position,spawner.rotation);
				break;
			//Currently can only spawn straight or spinning platforms no corners
			/*case 2:
				Instantiate (PlatformCorner,spawner.position,spawner.rotation);
				break;*/
			}


		}
	}
}
