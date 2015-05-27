using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AnnoyingFlasher : MonoBehaviour {

    public float speed = 4.0f;
    private float startTime = 0.0f;

	// Use this for initialization
	void Start () {
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
	    GetComponent<Image>().enabled = (((int)((Time.time-startTime)*speed) % 2) == 0);
	}
}
