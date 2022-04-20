using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Tooltip("Total score needed for the hole to get bigger")]
    public int ScoreNeeded = 10;

    [Header("UI Elements")]
    [Tooltip("Score text of the game")]
    [SerializeField] private Text _scoreText;

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

    public void AddScore(int score)
    {
        _totalScore += score;
        _scoreCounter += score;

        if (_scoreCounter >= ScoreNeeded)
        {
            _scoreCounter %= ScoreNeeded;
        }

        _playerController.ScaleHoleScale();
        _cameraFollowHole.AddZoomLevel();

        _scoreText.text = _totalScore.ToString();
    }
}
