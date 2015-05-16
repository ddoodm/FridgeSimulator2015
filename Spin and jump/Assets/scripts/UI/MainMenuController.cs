using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    public string mainSceneName = "SprintOneTest";

    public void onClick_Start()
    {
        Application.LoadLevel(mainSceneName);
    }

    public void onClick_Quit()
    {
        Application.Quit();
    }

    public void onClick_Tutorial()
    {
        Application.LoadLevel(2);
    }
}
