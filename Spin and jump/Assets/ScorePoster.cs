using UnityEngine;
using System.Collections;

public class ScorePoster : MonoBehaviour
{
    public string serverURI = "http://ddoodm.com/UnityProjects/SpinAndJumpSim/ScoreServ/postScore.php";
    private GameController gameController;
    private string username = "";

    void Start()
    {
        gameController = GetComponent<GameController>();
    }

    public void setUsername(string value)
    {
        this.username = value;
    }

    public void postScore()
    {
        int score = (int)gameController.score;
        int time = (int)gameController.gameDuration;

        if (username.Length == 0)
        {
            Debug.Log("Score Poster - No username");
            return;
        }

        StartCoroutine(postInBackground(score, username, time));
    }

    private IEnumerator postInBackground(int score, string username, int time)
    {
        // Create HTTP form header
        WWWForm form = new WWWForm();
        form.AddField("score", (int)gameController.score);
        form.AddField("name", username);
        form.AddField("time", (int)gameController.gameDuration);

        // Post to the PHP script on the server, which interfaces with SQL
        WWW www = new WWW(serverURI, form);
        yield return www;

        Debug.Log("Score Poster - Posted new score to the server.");
    }
}
