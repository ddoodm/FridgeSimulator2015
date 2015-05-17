using UnityEngine;
using System.Collections;

public class ActivateLater : MonoBehaviour
{
    public float targetSeconds = 0.5f;

    private float startTime;

	// Use this for initialization
	void Start () {
        startTime = Time.time;

        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        float dt = Time.time - targetSeconds;
        if (dt >= targetSeconds)
            for (int i = 0; i < transform.childCount; i++)
                transform.GetChild(i).gameObject.SetActive(true);
	}
}
