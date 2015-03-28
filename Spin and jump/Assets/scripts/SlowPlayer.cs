using UnityEngine;
using System.Collections;

public class SlowPlayer : MonoBehaviour
{
    public float slowSpeed = 0.025f;

	private PlayerController player;

<<<<<<< HEAD

	void OnTriggerEnter(Collider other)
=======
	void OnCollisionEnter(Collision c)
>>>>>>> da9c9017bf029b5a8b661a48843362b529862465
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
