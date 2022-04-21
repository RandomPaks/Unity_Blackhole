using System;
using System.Collections.Generic;
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
    [Tooltip("3D floating text of the game")]
    [SerializeField] private GameObject _floatingTextObject;
    [Tooltip("Number of floating texts to pool")]
    [SerializeField] private int _floatingTextPoolAmount;

    private int _scoreCounter;
    private int _totalScore;
    private List<GameObject> _floatingTextPool;

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

    private void Start()
    {
        //object pooling for text
        _floatingTextPool = new List<GameObject>();

        for (int i = 0; i < _floatingTextPoolAmount; i++)
        {
            GameObject newObject = Instantiate(_floatingTextObject, transform);
            newObject.SetActive(false);
            _floatingTextPool.Add(newObject);
        }
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
            _scoreCounter %= ScoreNeeded;
            ScoreNeeded *= ScoreNeededMultiplier;
            _playerController.ScaleHoleScale();
            _cameraFollowHole.AddZoomLevel();
        }

        ShowFloatingText(score);
        _scoreText.text = "Score: " + _totalScore.ToString();
    }

    private void ShowFloatingText(int score)
    {
        GameObject newObject = GetFloatingTextPoolObject();
        FloatingText floatingText = newObject.GetComponent<FloatingText>();

        floatingText.ResetFloatingText();
        floatingText.SetText("+" + score);
        newObject.SetActive(true);
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
}
