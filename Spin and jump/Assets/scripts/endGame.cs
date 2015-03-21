using UnityEngine;
using System.Collections;

public class endGame : MonoBehaviour {

	private GameController gameController;
	
	
	void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController> ();
		} else { 
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			gameController.GameOver();
			
		}
	}

}
