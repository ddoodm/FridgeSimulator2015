using UnityEngine;
using System.Collections;

public class GoToMainGame : MonoBehaviour
{
    public Flasher fader;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fader.flash();
            StartCoroutine(goToGameLater());
        }
    }

    IEnumerator goToGameLater()
    {
        yield return new WaitForSeconds(1.0f);

        Application.LoadLevel(1);
    }
}
