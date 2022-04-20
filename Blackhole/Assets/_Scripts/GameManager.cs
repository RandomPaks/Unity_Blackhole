using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int _totalScore;

    private PlayerController _playerController;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void AddScore(int score)
    {
        _totalScore += score;
        _playerController.ScaleHoleScale();
        Debug.Log("Score: " + _totalScore);
    }
}
