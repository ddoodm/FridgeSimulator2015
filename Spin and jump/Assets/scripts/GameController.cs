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

    public float gameStartTime, gameDuration;

    public Flasher flasher;

    void Start()
    {
        updateScore();

        gameStartTime = Time.time;
    }

    public void AddScore(float newScoreValue)
    {
        score += newScoreValue;
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

            // Update the game duration time
            gameDuration = Time.time - gameStartTime;
        }

        if (((int)score % (int)checkpointIncrement) == 0 && score >= 1.0f && (player.moveSpeed < speedCap))
        {
            player.moveSpeed += speedDelta;
            Debug.Log("Speed Increased AT " + checkpointIncrement + " TO " + player.moveSpeed);

            checkpointIncrement += checkpointIncrement * 0.5f;
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
