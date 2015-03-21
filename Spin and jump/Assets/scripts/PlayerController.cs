using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	public float jump;

	// Use this for initialization
	
	// Update is called once per frame
	void FixedUpdate()
	{
		float moveUp = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (0, moveUp, 0);
		rigidbody.AddForce (movement * jump * Time.deltaTime);
	}


}
