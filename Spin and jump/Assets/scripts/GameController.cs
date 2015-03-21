using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GUIText restartText;
	public GUIText gameOverText;
	public GUIText scoreText;

	private bool gameOver;
	private int score;

	void Start()
	{
		gameOver = false;
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		updateScore ();
		
	}

	public void AddScore(int newScoreValue)
	{
		score += newScoreValue;
		updateScore ();
	}
	public void GameOver()
	{
		gameOverText.text = "Game Over!";
		gameOver = true;
		restartText.text = "Press R to restart";
	}
	void updateScore()
	{
		scoreText.text = "Score: " + score;
	}
	void Update()
	{
		if (gameOver) {
			if (Input.GetKeyDown (KeyCode.R)) {
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}
}
