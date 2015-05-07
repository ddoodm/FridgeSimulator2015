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

	private bool gameOver = false;
	public float score = 0;

    public float
        checkpointIncrement = 200.0f,
        speedCap = 0.2f,
        speedDelta = 0.01f;

    public float gameStartTime, gameDuration;

	void Start()
	{
		restartText.text = "";
		gameOverText.text = "";
		updateScore ();

        gameStartTime = Time.time;
	}

	public void AddScore(float newScoreValue)
	{
		score += newScoreValue;
		updateScore ();
	}
	public void SetScore(float newScoreValue)
	{
		score = newScoreValue;
		updateScore ();
	}

	public void GameOver()
	{
		gameOverText.text = "GAME OVER";
		gameOver = true;
		restartText.text = "Press R to Restart";

        uiCanvas.gameObject.SetActive(true);
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
			/* Rob's distance-based counter. Disabled in order to allow score to increment regardless of position
            float dist = Vector3.Distance (player.transform.position, scorigin.position);
            SetScore(dist);
             */

            // New velocity-based scoring
            float dist = player.velocityMagnitude * scoreDelta;
			AddScore (dist);

            // Update the game duration time
            gameDuration = Time.time - gameStartTime;
		}

		if (gameOver && Input.GetKeyDown (KeyCode.R))
			Application.LoadLevel (Application.loadedLevel);

        if (((int)score % (int)checkpointIncrement) == 0 && score >= 1.0f && (player.moveSpeed < speedCap))
        {
            player.moveSpeed += speedDelta;
            Debug.Log("Speed Increased AT " + checkpointIncrement + " TO " + player.moveSpeed);

            checkpointIncrement += checkpointIncrement * 0.5f;
        }
	}
}
