using System;
using UI;
using UnityEngine;
using UnityEngine.UIElements;

namespace Managers
{
    public class ScoreTimeManager : MonoBehaviour
    {
        public static ScoreTimeManager Instance { get; private set; }
        public static event Action<int> OnScoreUpdated;
        public int Score { get; private set; }
        public float RemainingTime { get; private set; } = StartingTime;

        private const float StartingTime = 30;

        private bool _isLevelStarted;
        private UIDocument _levelUIDocument;
        private Label _timerValueLabel;

        private void OnEnable()
        {
            GameManager.OnGameStateChanged += ResetScoreAndTime;
            GameManager.OnGameStateChanged += InitScoreAndTimer;
            LevelUI.OnQuizAnsweredCorrectly += AddPoint;
        }

        private void OnDisable()
        {
            GameManager.OnGameStateChanged -= ResetScoreAndTime;
            GameManager.OnGameStateChanged -= InitScoreAndTimer;
            LevelUI.OnQuizAnsweredCorrectly -= AddPoint;
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
        }

        private void ResetScoreAndTime(GameManager.GameState gameState)
        {
            if (gameState != GameManager.GameState.StartPlaying)
                return;

            Score = 0;
            RemainingTime = StartingTime;
            print("ResetScoreAndTime");
        }

        private void InitScoreAndTimer(GameManager.GameState gameState)
        {
            if (gameState != GameManager.GameState.LevelLoaded)
                return;

            _isLevelStarted = true;
            OnScoreUpdated?.Invoke(Score);
            _timerValueLabel = (Label)
                FindObjectOfType<UIDocument>().rootVisualElement.Q("timer-value");
        }

        private void Update()
        {
            if (!_isLevelStarted)
                return;

            if (RemainingTime > 0)
                RemainingTime -= Time.deltaTime;
            else if (RemainingTime < 0)
            {
                RemainingTime = 0;
                GameManager.Instance.UpdateGameState(GameManager.GameState.LevelFinished);
            }

            int minutes = Mathf.FloorToInt(RemainingTime / 60);
            int seconds = Mathf.FloorToInt(RemainingTime % 60);
            _timerValueLabel.text = $"{minutes:0}:{seconds:00}";
        }

        private void AddPoint()
        {
            Score++;
            OnScoreUpdated?.Invoke(Score);
        }
    }
}
