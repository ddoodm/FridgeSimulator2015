using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public AudioSource jump, land, step, stepSlow, loop, loop2;
	PlayerController player;
	GameController gameController;
	bool wasInAir, wasPaused, wasOnPlatform = true;
	
	void Start () {
		player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
		gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();

        // Play music
        switch(PlayerPrefs.GetInt("MusicTrackID", 0))
        {
            case 0: loop.Play(); break;
            case 1: loop2.Play(); break;
        }
	}

	void Update () {
		this.rigidbody.position = player.rigidbody.position;

        if(gameController.paused)
        {
            step.Stop();
            stepSlow.Stop();
        }
        if (wasPaused && !gameController.paused && !player.isInAir)
            step.Play();

        if (Input.GetButton("Jump") && !player.isInAir && !gameController.paused)
        {
			jump.Play ();
			step.Stop();
			stepSlow.Stop();
		}

		if (((!wasOnPlatform && player.onPlatform && wasInAir && !player.isInAir) || (wasInAir && !player.isInAir)) && !land.isPlaying)
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
        wasPaused = gameController.paused;
	}

    public void swapTracks()
    {
        if (loop.isPlaying)
        {
            loop.Stop();
            loop2.Play();
            PlayerPrefs.SetInt("MusicTrackID", 1);
        }
        else
        {
            loop.Play();
            loop2.Stop();
            PlayerPrefs.SetInt("MusicTrackID", 0);
        }
    }
}