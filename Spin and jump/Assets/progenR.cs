using UnityEngine;
using System.Collections;

public class progenR : MonoBehaviour {
	
	public Transform spawnerN;
	public Transform spawnerE;
	public Transform spawnerW;
	public Transform spawnerS;
	public GameObject platformStraight;
	public GameObject platformCorner;
	
	void OnTriggerEnter(Collider other)
	{
		int spawnchoice = Random.Range (0, 1);
//		int choice = Random.Range (0, 1);  Eventually have it random between either straight or corner
		int choice = 0;
//		Debug.Log (choice);
		if (other.tag == "Player") {
			switch (spawnchoice)
			{
			case 0:
				switch(choice)
				{
				case 0:
					Instantiate (platformStraight,spawnerN.position,spawnerN.rotation);
					break;
				case 1:
					Instantiate (platformCorner,spawnerN.position,spawnerN.rotation);
					break;
				}
				break;
			case 1:
				switch(choice)
				{
				case 0:
					Instantiate (platformStraight,spawnerE.position,spawnerE.rotation);
					break;
				case 1:
					Instantiate (platformCorner,spawnerE.position,spawnerE.rotation);
					break;
				}
				break;
			case 2:
				switch(choice)
				{
				case 0:
					Instantiate (platformStraight,spawnerW.position,spawnerW.rotation);
					break;
				case 1:
					Instantiate (platformCorner,spawnerW.position,spawnerW.rotation);
					break;
				}
				break;
			case 3:
				switch(choice)
				{
				case 0:
					Instantiate (platformStraight,spawnerW.position,spawnerS.rotation);
					break;
				case 1:
					Instantiate (platformCorner,spawnerW.position,spawnerS.rotation);
					break;
				}
				break;
			}
			
			
		}
	}
}