using System;
using UnityEngine;
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

    [Header("UI Elements")]
    [Tooltip("Score text of the game")]
    [SerializeField] private Text _scoreText;
    [Tooltip("Score text of the game")]
    [SerializeField] private Text _timeText;

    private int _scoreCounter;
    private int _totalScore;

    private PlayerController _playerController;
    private Camera _mainCamera;
    private CameraFollowHole _cameraFollowHole;

    public PlayerController Player => _playerController;
    public Camera MainCamera => _mainCamera;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        _cameraFollowHole = _mainCamera.gameObject.GetComponent<CameraFollowHole>();
    }

    private void Update()
    {
        TimeLeft -= Time.deltaTime;

        TimeSpan time = TimeSpan.FromSeconds(TimeLeft);
        _timeText.text = string.Format("{0:D2}:{1:D2}", time.Minutes, time.Seconds);
    }

    public void AddScore(int score)
    {
        _totalScore += score;
        _scoreCounter += score;

        if (_scoreCounter >= ScoreNeeded)
        {
            ScoreNeeded *= ScoreNeededMultiplier;
            _scoreCounter %= ScoreNeeded;
            Debug.Log(ScoreNeeded);
            Debug.Log(_scoreCounter);
            _playerController.ScaleHoleScale();
            _cameraFollowHole.AddZoomLevel();
        }

        _scoreText.text = "Score: " + _totalScore.ToString();
    }
}
