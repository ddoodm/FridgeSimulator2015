using UnityEngine;
using System.Collections;

public class SlowOnContact : MonoBehaviour {

	private GameController gameController;
	private PlayerController playerController;
    private CamShaker camera;
    public AudioSource impact;

    public float slowValue = 0;

    void Start()
    {
		gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
		playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        camera = GameObject.FindWithTag("MainCamera").GetComponent<CamShaker>();
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
			float scoreRemove = playerController.moveSpeed * 10;
			gameController.RemoveScore(50.0f);
            impact.Play();

            //Destroy(gameObject);
            camera.shake();
        }
    }
}
