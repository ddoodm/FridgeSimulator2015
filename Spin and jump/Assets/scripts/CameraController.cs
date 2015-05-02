using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	public PlayerController player;
    public float butter = 0.1f;

	private Vector3 offset;
    private Vector3 offsetRot;

    private GameController gameController;

	// Use this for initialization
	void Start () 
	{
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();

		offset = transform.position;
        offsetRot = transform.rotation.eulerAngles;
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
        // Do not follow if the game is over
        if (gameController.isGameOver)
            return;

        transform.rotation = Quaternion.identity;

        Vector3 target = player.transform.position + player.velocity;
        Vector3 dampPlayerPos = (transform.position - target) * butter * Time.deltaTime;

        //transform.position -= dampPlayerPos;
        //transform.Rotate(Vector3.up, player.transform.rotation.eulerAngles.y);
        transform.Translate(-dampPlayerPos);
        transform.Rotate(player.transform.up, player.transform.rotation.eulerAngles.y);
        transform.Translate(offset * butter * Time.deltaTime);
        transform.LookAt(player.rigidbody.position + Vector3.up * 2.0f);
        //transform.RotateAround(player.transform.position, player.transform.up, Time.time*4.0f);
        //transform.Translate(offset);

        transform.Rotate(offsetRot);

        // Lock Y transformation
        //Vector3 yLock = new Vector3(1.0f, 0.0f, 1.0f);
        //transform.position = Vector3.Scale(transform.position, yLock) + Vector3.Scale(offset, Vector3.up);
	}
}
