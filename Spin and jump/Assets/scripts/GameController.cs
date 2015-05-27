using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public ShadowText restartText;
    public ShadowText gameOverText;
    public ShadowText scoreText;

    /// <summary>
    /// The main player controller.
    /// </summary>
    public PlayerController player;
    public Transform scorigin;

    /// <summary>
    /// The amount of score to add each frame the player is alive.
    /// </summary>
    public float scoreDelta = 0.25f;

    public Canvas uiCanvas;
    public Canvas pauseCanvas;

    private bool gameOver = false;
    public float score = 0;

    public bool paused = false;

    public float
        checkpointIncrement = 200.0f,
        speedCap = 0.2f,
        speedDelta = 0.01f;

	// Difficulty Variables sent to dPathGen
	public float difficulty = 0.0f;
	public float speedDifficulty = 5.0f;
	public float difficultyDelta = 1.0f;

    public float gameStartTime, gameDuration;

    public Flasher flasher;

    void Start()
    {
		difficulty = PlayerPrefs.GetFloat ("difficulty");

		speedDifficulty = PlayerPrefs.GetFloat ("SpeedDifficulty", 5.0f);

		player.moveSpeed = speedDifficulty;


		for (int i = 0; i < speedDifficulty - 5; i++) {
			checkpointIncrement += checkpointIncrement * 0.5f;
		}


		
		if (difficulty >= 2800) {
			difficulty = 4000;
		}

        updateScore();

        gameStartTime = Time.time;
    }

    public void AddScore(float newScoreValue)
    {
		newScoreValue += newScoreValue * difficultyDelta;
        score += newScoreValue;
        updateScore();
    }

	public void CalculateDifficulty(float newDifficultyValue)
	{
		difficulty += newDifficultyValue;
		speedDifficulty += newDifficultyValue;
		difficultyDelta = difficulty / 10000;
		difficultyDelta += difficultyDelta * (Mathf.Pow (difficulty / 10000, 2.0f));
	}

	public float getDifficulty()
	{
		return difficulty / 100;
	}

	public void RemoveScore(float newScoreValue)
	{
		score -= newScoreValue;
		if (score < 0) {
			score = 0;
		}
		updateScore();
	}
	
    public void SetScore(float newScoreValue)
    {
        score = newScoreValue;
        updateScore();
    }

    public void GameOver()
    {
        gameOver = true;
        uiCanvas.gameObject.SetActive(true);
        flasher.flash();

        paused = false;
        pauseCanvas.gameObject.SetActive(false);
    }

    public bool isGameOver
    {
        get { return gameOver; }
    }

    void updateScore()
    {
        // Show the score as a whole integer
        scoreText.text = string.Format("{0}", Mathf.Floor(score));
    }

    void Update()
    {
        // The score integral is the player's time alive
        if (!gameOver)
        {
            // Pause if required
            if (Input.GetKeyDown(KeyCode.Escape))
                pauseCanvas.gameObject.SetActive(paused = !paused);
            if (paused)
            {
                Time.timeScale = .0f;
                return;
            }
            else Time.timeScale = 1.0f;

            // New velocity-based scoring
            float dist = player.velocityMagnitude * scoreDelta;
            AddScore(dist);
			CalculateDifficulty(dist);

            // Update the game duration time
            gameDuration = Time.time - gameStartTime;

			if (((int)speedDifficulty % (int)checkpointIncrement) == 0 && (player.moveSpeed < speedCap))
			{
				player.moveSpeed += speedDelta;
				Debug.Log("Speed Increased AT " + checkpointIncrement + " TO " + player.moveSpeed);
				
				checkpointIncrement += checkpointIncrement * 0.5f;
			}
        }
    }

    public void restartGame()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void resumeGame()
    {
        paused = false;
        pauseCanvas.gameObject.SetActive(false);
    }

    public void mainMenu()
    {
        Application.LoadLevel(0);
        paused = false;
    }
}
