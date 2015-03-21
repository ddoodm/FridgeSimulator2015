using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	public GameObject player;
	private Vector3 offset;

    private GameController gameController;

	// Use this for initialization
	void Start () 
	{
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();

		offset = transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
        // Do not follow if the game is over
        if (gameController.isGameOver)
            return;

        // Lock transformation in the Y axis
        Vector3 yLock = new Vector3(1.0f, 0.0f, 1.0f);
		transform.position = Vector3.Scale(player.transform.position, yLock) + offset;
	}
}
