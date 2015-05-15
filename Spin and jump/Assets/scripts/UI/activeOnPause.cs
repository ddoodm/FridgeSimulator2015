using UnityEngine;
using System.Collections;

public class activeOnPause : MonoBehaviour {

    private GUIText pauseText;

    private GameController gameController;

	// Use this for initialization
	void Start () {
        pauseText = GetComponent<GUIText>();
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
	
	}
	
	// Update is called once per frame
    void Update()
    {
        pauseText.enabled = gameController.paused;
    }

}
