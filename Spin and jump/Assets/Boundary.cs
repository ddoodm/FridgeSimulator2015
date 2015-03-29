using UnityEngine;
using System.Collections;

public class Boundary : MonoBehaviour {

	void OnTriggerExit(Collider other)
	{
		Destroy (other.gameObject);
	}
}
