using UnityEngine;
using System.Collections;

public class mover : MonoBehaviour {
	
	public float speed;
	
	void Start()
	{
		rigidbody.velocity = transform.forward * speed;
	}
}
