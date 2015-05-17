using UnityEngine;
using System.Collections;

public class SlowOnContact : MonoBehaviour {

	private GameController gameController;
	private PlayerController playerController;

    public float slowValue = 0;

    void Start()
    {
		gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
		playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
			float scoreRemove = playerController.moveSpeed * 10;
			gameController.RemoveScore(50.0f);

            Destroy(gameObject);
        }
    }
}
