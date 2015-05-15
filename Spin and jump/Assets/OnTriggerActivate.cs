using UnityEngine;
using System.Collections;

public class OnTriggerActivate : MonoBehaviour {

    public GameObject activate;
    public bool state = false;

	void Update () {
        activate.SetActive(state);	
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            state = true;
        }
    }
}
