using System;
using UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public static event Action<int> ScoreUpdated;
    private int _score;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        LevelUI.QuizAnsweredCorrectly += addPoint;
    }

    private void OnDisable()
    {
        LevelUI.QuizAnsweredCorrectly -= addPoint;
    }

    private void Start()
    {
        ScoreUpdated?.Invoke(_score);
    }

    private void addPoint()
    {
        _score++;
        ScoreUpdated?.Invoke(_score);
    }
}
