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

	private bool gameOver = false;
	private float score = 0;

	void Start()
	{
		restartText.text = "";
		gameOverText.text = "";
		updateScore ();
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
			float dist = Vector3.Distance (player.transform.position, scorigin.position);
            SetScore(dist);

            /* Use this instead for velocity-based scoring
            float dist = player.velocityMagnitude * scoreDelta;
			AddScore (dist);
             */
		}

		if (gameOver && Input.GetKeyDown (KeyCode.R))
			Application.LoadLevel (Application.loadedLevel);
	}
}
