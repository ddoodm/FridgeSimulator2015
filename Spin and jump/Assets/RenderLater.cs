using UnityEngine;
using System.Collections;

public class RenderLater : MonoBehaviour {

    public float delay = 0.5f;

    private float startTime;

	// Use this for initialization
	void Start () {
        startTime = Time.time;
        renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        float dt = Time.time - startTime;
        if (dt >= delay)
            renderer.enabled = true;
	}
}
