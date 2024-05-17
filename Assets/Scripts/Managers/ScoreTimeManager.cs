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
        public float RemainingTime { get; private set; }

        private const float StartingTime = 1200;

        private bool _isLevelStarted;
        private UIDocument _levelUIDocument;
        private Label _timerValueLabel;

        public static string FormatTime(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);
            return $"{minutes:0}:{seconds:00}";
        }

        private void OnEnable()
        {
            GameManager.OnGameStateChanged += InitScoreAndTimer;
            GameManager.OnGameStateChanged += StopTime;
            LevelUI.OnQuizAnsweredCorrectly += AddPoint;
        }

        private void OnDisable()
        {
            GameManager.OnGameStateChanged -= InitScoreAndTimer;
            GameManager.OnGameStateChanged -= StopTime;
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

        public void ResetScoreTime()
        {
            Score = 0;
            RemainingTime = StartingTime;
            _isLevelStarted = false;
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

        private void StopTime(GameManager.GameState gameState)
        {
            if (gameState != GameManager.GameState.LevelFinished)
                return;

            _isLevelStarted = false;
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
            _timerValueLabel.text = FormatTime(RemainingTime);
        }

        private void AddPoint()
        {
            Score++;
            OnScoreUpdated?.Invoke(Score);
        }
    }
}
