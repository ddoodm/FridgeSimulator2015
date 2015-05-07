using UnityEngine;
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

    public Score[] scores;

	IEnumerator Start ()
    {
        // Wait until the document has been returned
        WWW www = new WWW(getHighscoreURI);
        yield return www;

        // Split HTML rows into an array
        string[] rows = www.text.Split(new string[]{"<br/>"}, System.StringSplitOptions.RemoveEmptyEntries);

        scores = new Score[rows.Length];
        for(int i=0; i<rows.Length; i++)
        {
            string[] cols = rows[i].Split(' ');
            int score = int.Parse(cols[0]);
            string name = cols[1];
            int time = int.Parse(cols[2]);
            scores[i] = new Score(score, name, time);
        }
	}

    void OnGUI()
    {
        if (scores == null)
            return;

        foreach (Score score in scores)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(score.score.ToString(), GUILayout.Width(colWidth));
            GUILayout.Label(score.name, GUILayout.Width(colWidth));
            GUILayout.EndHorizontal();
        }
    }
}
