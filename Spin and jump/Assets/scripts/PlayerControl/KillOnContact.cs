using UnityEngine;
using System.Collections;

public class KillOnContact : MonoBehaviour
{
    private GameController gameController;
    private Flasher flasher;

    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        flasher = GameObject.FindWithTag("Flasher").GetComponent<Flasher>();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            gameController.GameOver();
            flasher.flash();
            col.gameObject.SetActive(false);
            Debug.Log(string.Format("{0} contacted the player, resulting in death.", this.gameObject));
        }
    }
}
