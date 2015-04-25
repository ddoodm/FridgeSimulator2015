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

	public int direction = -1; // -1 is left, 1 is right

	void Update ()
    {
		deltaRotation = direction * speed * Time.deltaTime;
		transform.Rotate(axis * deltaRotation);
	}
	
}
