using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour
{
    /// <summary>
    /// The axis to rotate about
    /// </summary>
    public Vector3 axis = Vector3.up;
    
    /// <summary>
    /// The speed at which to rotate
    /// </summary>
    public float speed = 30.0f;

    public float deltaRotation { get; protected set; }

	private int direction = 0; // 0 is right, 1 is left
	void Start()
	{
		direction = Random.Range (0, 2);
		Debug.Log (direction);

	}
	void Update ()
    {
		switch (direction) {
		case 1:
			deltaRotation = -speed * Time.deltaTime;
			transform.Rotate(axis * deltaRotation);
			break;
		default:
			deltaRotation = speed * Time.deltaTime;
			transform.Rotate(axis * deltaRotation);
			break;

		}

	}
	
}
