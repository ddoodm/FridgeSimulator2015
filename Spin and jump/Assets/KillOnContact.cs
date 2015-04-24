using UnityEngine;
using System.Collections;

public class KillOnContact : MonoBehaviour
{
    private GameController gameController;

    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            gameController.GameOver();
            col.gameObject.SetActive(false);
            Debug.Log(string.Format("{0} contacted the player, resulting in death.", this.gameObject));
        }
    }
}
