using UnityEngine;
using System.Collections;

public class FlashOnStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Flasher>().flash();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
