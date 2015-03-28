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

	void Update ()
    {
        deltaRotation = speed * Time.deltaTime;
        transform.Rotate(axis * deltaRotation);
	}
	
}
