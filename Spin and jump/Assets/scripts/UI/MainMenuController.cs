using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    public string mainSceneName = "SprintOneTest";
    public AudioSource buttonPress;

	public float difficulty = 0.0f;
	public float speedDifficulty = 5.0f;

    public void onClick_Start()
    {
		PlayerPrefs.SetFloat("difficulty", difficulty);
		PlayerPrefs.SetFloat("SpeedDifficulty", speedDifficulty);

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

	public void onSlide_Difficulty(float sliderDifficulty)
	{
		difficulty = sliderDifficulty;
	}

	public void onSlide_SpeedDifficulty(float sliderSpeedDifficulty)
	{
		speedDifficulty = sliderSpeedDifficulty;
	}


}
