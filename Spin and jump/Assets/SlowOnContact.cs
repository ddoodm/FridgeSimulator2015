using UnityEngine;
using System.Collections;

public class SlowOnContact : MonoBehaviour {

    private PlayerController playerController;

    public float slowValue = 0;

    void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerController.moveSpeed -= slowValue;
            Destroy(gameObject);
        }
    }
}
