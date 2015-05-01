using UnityEngine;
using System.Collections;

public class activeOnPause : MonoBehaviour {

    private GUIText pauseText;

    private PlayerController playerController;

	// Use this for initialization
	void Start () {
        pauseText = GetComponent<GUIText>();
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
	
	}
	
	// Update is called once per frame
    void Update()
    {
        pauseText.enabled = playerController.paused;
    }

}
