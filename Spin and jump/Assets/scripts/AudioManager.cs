using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public AudioSource jump, land, step, stepSlow;
	PlayerController player;
	GameController gameController;
	bool wasInAir, wasOnPlatform = true;
	
	void Start () {
		player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
		gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
	}

	void Update () {
		this.rigidbody.position = player.rigidbody.position;

		if (Input.GetButton ("Jump") && !player.isInAir && !player.paused) {
			jump.Play ();
			step.Stop();
			stepSlow.Stop();
		}

		if (((!wasOnPlatform && player.onPlatform) || (wasInAir && !player.isInAir)) && !land.isPlaying)
			land.Play();

		if (wasInAir && !player.isInAir && !player.isSlowed) {
			step.Play ();
		} 
		else if (gameController.isGameOver)
			step.Stop ();

		if (!player.isSlowed) {// && player.isInAir == !wasInAir) {
			stepSlow.Play ();
			wasInAir = player.isInAir;
		}

        wasOnPlatform = player.onPlatform;
        wasInAir = player.isInAir;
	}
}