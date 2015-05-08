using UnityEngine;
using System.Collections;

public class progenC : MonoBehaviour {
	
	public Transform spawner;
	public GameObject platformStraight;
	public GameObject platformRotating;
//	public GameObject PlatformCorner;
	
	void OnTriggerEnter(Collider other)
	{
		int choice = Random.Range (0, 2);

		Quaternion locR = spawner.rotation;
		//locR.y = 45;

		if (other.tag == "Fake") {
			other.GetComponent<fakePlayer>().turnLeft ();
			switch(choice)
			{
			case 0:
				Instantiate (platformStraight,spawner.position,locR);
				Debug.Log ("spawn straight");
				break;
			case 1:
				Instantiate (platformRotating,spawner.position,locR);
				Debug.Log ("spawn spin");
				break;
				
			default:
				Instantiate (platformStraight,spawner.position,locR);
				break;
				//Currently can only spawn straight or spinning platforms no corners
				
			}
			
			
		}
	}
}
