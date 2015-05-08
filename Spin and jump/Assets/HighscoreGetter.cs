using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score
{
    public string name;
    public int score, time;

    public Score(int score, string name, int time)
    {
        this.name = name; this.score = score; this.time = time;
    }
}

public class HighscoreGetter : MonoBehaviour
{
    public string getHighscoreURI = "http://ddoodm.com/UnityProjects/SpinAndJumpSim/ScoreServ/highscores.php";
    public float colWidth = 128.0f;
    public int limit = 10;

    public Score[] scores;

    public Text
        scoreText,
        timeText,
        nameText;

	void Start ()
    {
        refreshInBackground();
	}

    public void refreshInBackground()
    {
        StartCoroutine(refreshScores());
    }

    public IEnumerator refreshScores()
    {
        // Wait until the document has been returned
        string uriGet = getHighscoreURI + string.Format("?lim={0}", limit);
        WWW www = new WWW(uriGet);
        yield return www;

        // Split HTML rows into an array
        string[] rows = www.text.Split(new string[] { "<br/>" }, System.StringSplitOptions.RemoveEmptyEntries);

        scores = new Score[rows.Length];
        for (int i = 0; i < rows.Length; i++)
        {
            string[] cols = rows[i].Split(' ');
            int score = int.Parse(cols[0]);
            string name = cols[1];
            int time = int.Parse(cols[2]);
            scores[i] = new Score(score, name, time);
        }

        updateTextFields();
    }

    private void updateTextFields()
    {
        scoreText.text = timeText.text = nameText.text = "";

        foreach(Score score in scores)
        {
            scoreText.text += score.score.ToString() + "\n";
            timeText.text += score.time.ToString() + "\n";
            nameText.text += score.name + "\n";
        }
    }
}
