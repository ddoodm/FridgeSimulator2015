using UnityEngine;
using System.Collections;

public class SinBouncer : MonoBehaviour
{
    public float frequency = 10.0f;
    public float amplitude = 0.5f;

    private Vector3 origin;

    void Start()
    {
        origin = transform.position;
    }

	void Update ()
    {
        transform.position = origin + amplitude * Vector3.up * Mathf.Abs(Mathf.Sin(Time.time * frequency));
	}
}
