using UnityEngine;
using System.Collections;

public class fakePlayer : MonoBehaviour {

	public float
		moveSpeed = 0.1f,
		slowedSpeed = 0.025f,
		correctionSpeed = 0.01f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LateUpdate()
	{
		rigidbody.position += transform.forward * moveSpeed;

		float rightAngle = Mathf.Round(transform.rotation.eulerAngles.y / 90.0f) * 90.0f;
		float rightDelta = transform.rotation.eulerAngles.y - rightAngle;
		transform.Rotate(Vector3.up, -rightDelta * correctionSpeed);
	}
	
	public void turnLeft()
	{
		transform.Rotate (new Vector3 (0, 270, 0));
		Debug.Log ("left");
	}

	public void turnRight()
	{
		Debug.Log ("right");
		transform.Rotate (new Vector3 (0, 90, 0));
	}

	public void turn(Vector3 loc)
	{

		transform.LookAt(loc);


	}
}
