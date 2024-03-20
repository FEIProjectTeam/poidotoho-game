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

        [SerializeField]
        private UIDocument _levelUIDocument;

        [SerializeField]
        private float _remainingTime;

        private Label _timerValueLabel;
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
            LevelUI.OnQuizAnsweredCorrectly += addPoint;
        }

        private void OnDisable()
        {
            LevelUI.OnQuizAnsweredCorrectly -= addPoint;
        }

        private void Start()
        {
            OnScoreUpdated?.Invoke(_score);
            _timerValueLabel = (Label)_levelUIDocument.rootVisualElement.Q("timer-value");
        }

        private void Update()
        {
            if (_remainingTime > 0)
                _remainingTime -= Time.deltaTime;
            else if (_remainingTime < 0)
                _remainingTime = 0;

            int minutes = Mathf.FloorToInt(_remainingTime / 60);
            int seconds = Mathf.FloorToInt(_remainingTime % 60);
            _timerValueLabel.text = $"{minutes:0}:{seconds:00}";
        }

        private void addPoint()
        {
            _score++;
            OnScoreUpdated?.Invoke(_score);
        }
    }
}
