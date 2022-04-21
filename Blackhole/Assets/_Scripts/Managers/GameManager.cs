using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Tooltip("Initial total score needed for the hole to get bigger")]
    public int ScoreNeeded = 10;
    [Tooltip("Multipler for score needed to add how much they need for the next hole expansion")]
    public int ScoreNeededMultiplier = 5;
    [Tooltip("How many seconds the player gets to play")]
    public float TimeLeft = 120;

    [Header("Floating Text Object")]
    [Tooltip("Number of floating texts to pool")]
    public int FloatingTextPoolAmount = 50;
    [Tooltip("3D floating text of the game")]
    [SerializeField] private GameObject _floatingTextObject;

    [Header("UI Elements")]
    [Tooltip("Score text of the game")]
    [SerializeField] private Text _scoreText;
    [Tooltip("Score text of the game")]
    [SerializeField] private Text _timeText;
    [Tooltip("End screen of the game")]
    [SerializeField] private GameObject _endScreenObject;
    [Tooltip("Total Score text of the end screen")]
    [SerializeField] private Text _totalScoreText;

    private int _scoreCounter;
    private int _totalScore;
    private List<GameObject> _floatingTextPool;
    private bool _isGameFinished;

    private PlayerController _playerController;
    private Camera _mainCamera;
    private CameraController _cameraController;

    public PlayerController Player => _playerController;
    public Camera MainCamera => _mainCamera;
    public bool IsGameFinished => _isGameFinished;
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        _cameraController = _mainCamera.gameObject.GetComponent<CameraController>();
    }

    private void Start()
    {
        //object pooling for text
        _floatingTextPool = new List<GameObject>();

        for (int i = 0; i < FloatingTextPoolAmount; i++)
        {
            GameObject newObject = Instantiate(_floatingTextObject, transform);
            newObject.SetActive(false);
            _floatingTextPool.Add(newObject);
        }
    }

    private void Update()
    {
        if (TimeLeft > 0)
        {
            TimeLeft -= Time.deltaTime;

            TimeSpan time = TimeSpan.FromSeconds(TimeLeft);
            _timeText.text = string.Format("{0:D2}:{1:D2}", time.Minutes, time.Seconds);
        }
        else
        {
            //check if the game has finished to run EndGame() once
            if (!_isGameFinished)
            {
                _isGameFinished = true;
                EndGame();
            } 
        }
    }

    public void AddScore(int score)
    {
        if (!_isGameFinished)
        {
            _totalScore += score;
            _scoreCounter += score;

            if (_scoreCounter >= ScoreNeeded)
            {
                _scoreCounter %= ScoreNeeded;
                ScoreNeeded *= ScoreNeededMultiplier;
                _playerController.ScaleHoleScale();
                _cameraController.AddZoomLevel();
            }

            ShowFloatingText(score);
            _scoreText.text = "Score: " + _totalScore.ToString();
        }
    }

    private void ShowFloatingText(int score)
    {
        GameObject newObject = GetFloatingTextPoolObject();

        if(newObject != null)
        {
            FloatingText floatingText = newObject.GetComponent<FloatingText>();

            floatingText.ResetFloatingText();
            floatingText.SetText("+" + score);
            newObject.SetActive(true);
        }
    }

    private GameObject GetFloatingTextPoolObject()
    {
        for (int i = 0; i < _floatingTextPool.Count; i++)
        {
            if (!_floatingTextPool[i].activeInHierarchy)
            {
                return _floatingTextPool[i];
            }
        }
        return null;
    }

    private void EndGame()
    {
        _scoreText.gameObject.SetActive(false);
        _timeText.gameObject.SetActive(false);

        _endScreenObject.SetActive(true);
        _totalScoreText.text = _scoreText.text;
        _playerController.GetComponent<PlayerController>().enabled = false;
    }

    public void OnButtonPlay() => SceneManager.LoadScene("GameScene");

    public void OnButtonExit() => Application.Quit();
}
