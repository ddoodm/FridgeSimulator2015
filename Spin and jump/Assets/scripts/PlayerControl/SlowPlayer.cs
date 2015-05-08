using UnityEngine;
using System.Collections;

public class SlowPlayer : MonoBehaviour
{
    public float slowSpeed = 0.025f;

	private PlayerController player;

	void OnCollisionEnter(Collision c)
	{
		if (c.gameObject.tag == "Player")
        {
			player = c.gameObject.GetComponent<PlayerController>();
            player.slowTo(slowSpeed);
		}
	}

    void OnCollisionExit(Collision c) 
	{
        if (c.gameObject.tag == "Player")
            player.stopSlow();
	}
}
