using System;
using UI;
using UnityEngine;
using UnityEngine.UIElements;

namespace Managers
{
    public class ScoreTimeManager : MonoBehaviour
    {
        public static event Action<int> OnScoreUpdated;
        public int Score { get; private set; }
        public float RemainingTime { get; private set; }

        [SerializeField]
        private UIDocument _levelUIDocument;
        private Label _timerValueLabel;

        private void OnEnable()
        {
            LevelUI.OnQuizAnsweredCorrectly += AddPoint;
        }

        private void OnDisable()
        {
            LevelUI.OnQuizAnsweredCorrectly -= AddPoint;
        }

        private void Start()
        {
            Score = GameManager.Instance.Score;
            RemainingTime = GameManager.Instance.RemainingTime;
            OnScoreUpdated?.Invoke(Score);

            _timerValueLabel = (Label)_levelUIDocument.rootVisualElement.Q("timer-value");
        }

        private void Update()
        {
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
