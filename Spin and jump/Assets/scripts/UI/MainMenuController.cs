using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    public string mainSceneName = "SprintOneTest";
    public AudioSource buttonPress;

    public void onClick_Start()
    {
        buttonPress.Play();
        Application.LoadLevel(mainSceneName);
    }

    public void onClick_Quit()
    {
        buttonPress.Play();
        Application.Quit();
    }

    public void onClick_Tutorial()
    {
        buttonPress.Play();
        Application.LoadLevel(2);
    }
}
