using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Highscore
{
    public string name;
    public int score;
}

[Serializable]
public class Highscores
{
    public List<Highscore> highscores;
}

public class LeaderboardManager : MonoBehaviour
{
    [Tooltip("Number of players listed on the highscore")]
    [Min(1)]
    [SerializeField] public int PlayerCount = 10;

    [Header("UI Elements")]
    [Tooltip("Where to parent the highscore texts")]
    [SerializeField] private Transform _scoresPanel;
    [Tooltip("Prefab for the highscore text")]
    [SerializeField] private GameObject _highscoreText;
    [Tooltip("Submit gameobject button for the score")]
    [SerializeField] private GameObject _submitScoreButton;
    [Tooltip("Input field for the player name")]
    [SerializeField] private InputField _nameField;

    private List<Highscore> _highscores;
    private GameObject[] _highscoreTexts;

    private void Awake()
    {
        _highscores = LoadHighscores();

        //create new highscore list if there's no list
        if (_highscores == null)
            _highscores = new List<Highscore>();

        //instantiate and update the highscores
        _highscoreTexts = new GameObject[PlayerCount];

        for (int i = 0; i < PlayerCount; i++)
        {
            _highscoreTexts[i] = Instantiate(_highscoreText, _scoresPanel);

            if(_highscores != null && _highscores.Count > i)
            {
                Text highscoreText = _highscoreTexts[i].GetComponent<Text>();
                highscoreText.text = _highscores[i].name + " : " + _highscores[i].score;
            }
            else
            {
                Text highscoreText = _highscoreTexts[i].GetComponent<Text>();
                highscoreText.text = "Player : 0";
            }
        }
    }

    public void AddHighscore()
    {
        Highscore highscore = new Highscore
        {
            name = _nameField.text,
            score = GameManager.Instance.TotalScore
        };
        _highscores.Add(highscore);
    }

    public void OnSubmitScore()
    {
        _submitScoreButton.SetActive(false);
        _nameField.gameObject.SetActive(false);

        AddHighscore();

        _highscores.Sort(SortByHighscore);

        //update text
        for (int i = 0; i < PlayerCount; i++)
        {
            if (_highscores != null && _highscores.Count > i)
            {
                Text highscoreText = _highscoreTexts[i].GetComponent<Text>();
                highscoreText.text = _highscores[i].name + " : " + _highscores[i].score;
            }
            else
            {
                Text highscoreText = _highscoreTexts[i].GetComponent<Text>();
                highscoreText.text = "Player : 0";
            }
        }

        SaveHighscores(_highscores);
    }

    private void SaveHighscores(List<Highscore> highscores)
    {
        Highscores newHighscores = new Highscores { highscores = highscores };
        string json = JsonUtility.ToJson(newHighscores, true);

        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    private List<Highscore> LoadHighscores()
    {
        if (!PlayerPrefs.HasKey("highscoreTable")) return null;

        string json = PlayerPrefs.GetString("highscoreTable");

        return JsonUtility.FromJson<Highscores>(json).highscores;
    }

    private int SortByHighscore(Highscore a, Highscore b)
    {
        return b.score.CompareTo(a.score);
    }
}