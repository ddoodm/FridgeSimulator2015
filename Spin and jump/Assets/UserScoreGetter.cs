using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable()]
public class PlayerNameStore
{
    public string name;
}

public class UserScoreGetter : MonoBehaviour
{
    public string userScoreURI = "http://ddoodm.com//UnityProjects/SpinAndJumpSim/ScoreServ/scoresForPlayer.php";
    public int limit = 8;

    public Score[] scores;

    public GameObject
        scoreDisplay,
        message;

    public Text
        scoreText,
        timeText;

    private string playerName;

    void Start()
    {
        // Get the player's name from the local file
        Debug.Log(Application.persistentDataPath);
        FileStream file = File.Open(Application.persistentDataPath + "/username.dat", FileMode.Open);
        if (file == null)
            return;

        BinaryFormatter bf = new BinaryFormatter();
        PlayerNameStore data = (PlayerNameStore)bf.Deserialize(file);
        file.Close();
        playerName = data.name;

        refreshInBackground();
    }

    public void updatePlayerName(string name)
    {
        FileStream file = File.Open(Application.persistentDataPath + "/username.dat", FileMode.OpenOrCreate);

        PlayerNameStore data = new PlayerNameStore();
        data.name = name;

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();

        playerName = name;

        refreshInBackground();
    }

    public void refreshInBackground()
    {
        if (playerName == null || playerName.Length < 1)
        {
            message.SetActive(true);
            return;
        }

        message.SetActive(false);
        scoreDisplay.SetActive(true);
        StartCoroutine(refreshScores());
    }

    public IEnumerator refreshScores()
    {
        // Wait until the document has been returned
        string uriGet = userScoreURI + string.Format("?name={0}&lim={1}", playerName, limit);
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
        scoreText.text = timeText.text = "";

        foreach (Score score in scores)
        {
            scoreText.text += score.score.ToString() + "\n";
            timeText.text += score.time.ToString() + "\n";
        }
    }
}
