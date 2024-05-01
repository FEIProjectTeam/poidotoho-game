using System;
using System.Collections.Generic;
using Incidents;
using Managers;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class LevelUI : MonoBehaviour
    {
        public static event Action OnQuizAnsweredCorrectly;

        [SerializeField]
        private UIDocument _document;

        [SerializeField]
        private StyleSheet _styleSheet;

        private List<Button> _answerBtns;
        private List<Button> _selectedAnswerBtns;
        private List<Image> _incidentSymbols;
        private Button _submitBtn;

        private void Awake()
        {
            var root = _document.rootVisualElement;
            root.Clear();
            root.styleSheets.Add(_styleSheet);

            var scoreContainer = Utils.Create(addTo: root, "score-container");
            Utils.Create<Label>(addTo: scoreContainer, "score-text").text = "body:";
            var scorePointsLabel = Utils.Create<Label>(addTo: scoreContainer, "score-points");
            scorePointsLabel.name = "score-points";
            scorePointsLabel.text = "0";

            Utils.Create(addTo: root, "incidents-container").name = "incidents-container";

            var timerContainer = Utils.Create(addTo: root, "timer-container");
            Utils.Create<Label>(addTo: timerContainer, "timer-text").text = "čas:";
            var timerValueLabel = Utils.Create<Label>(addTo: timerContainer, "timer-value");
            timerValueLabel.name = "timer-value";
            timerValueLabel.text = "∞";

            Utils.Create(addTo: root, "quiz-container").name = "quiz-container";
        }

        private void OnEnable()
        {
            IncidentBase.OnIncidentFound += OpenQuiz;
            ScoreTimeManager.OnScoreUpdated += UpdateScore;
            IncidentManager.OnIncidentsSpawned += CreateIncidentSymbols;
        }

        private void OnDisable()
        {
            IncidentBase.OnIncidentFound -= OpenQuiz;
            ScoreTimeManager.OnScoreUpdated -= UpdateScore;
            IncidentManager.OnIncidentsSpawned -= CreateIncidentSymbols;
        }

        private void OpenQuiz(IncidentBase incident)
        {
            if (GameManager.Instance.State == GameManager.GameState.DoingQuiz)
                return;
            GameManager.Instance.UpdateGameState(GameManager.GameState.DoingQuiz);

            var container = _document.rootVisualElement.Q("quiz-container");
            container.Clear();

            var containerBox = Utils.Create(addTo: container, "quiz-container-box");

            // Close button
            var closeBtn = Utils.Create<Button>(addTo: containerBox, "quiz-close-btn");
            closeBtn.text = "X";
            closeBtn.clicked += () =>
            {
                container.Clear();
                GameManager.Instance.UpdateGameState(GameManager.GameState.RoamingMap);
            };

            // Question
            var questionBox = Utils.Create(addTo: containerBox, "quiz-question-box");
            var questionLabel = Utils.Create<Label>(addTo: questionBox);
            questionLabel.text = incident.ActiveQNA.Question;

            // Answers
            var answerBox = Utils.Create(addTo: containerBox, "quiz-answer-box");
            _answerBtns = new List<Button>();
            _selectedAnswerBtns = new List<Button>();
            foreach (string answerText in incident.ActiveQNA.GetShuffledAnswers())
            {
                var answerBtn = Utils.Create<Button>(addTo: answerBox, "quiz-answer-btn", "active");
                answerBtn.text = answerText;
                answerBtn.clicked += () =>
                {
                    if (_selectedAnswerBtns.Contains(answerBtn))
                    {
                        _selectedAnswerBtns.Remove(answerBtn);
                        answerBtn.RemoveFromClassList("quiz-answer-selected");
                    }
                    else
                    {
                        _selectedAnswerBtns.Add(answerBtn);
                        answerBtn.AddToClassList("quiz-answer-selected");
                    }

                    _submitBtn.SetEnabled(_selectedAnswerBtns.Count > 0);
                };
                _answerBtns.Add(answerBtn);
            }

            // Submit button
            _submitBtn = Utils.Create<Button>(addTo: containerBox, "quiz-submit-btn");
            _submitBtn.text = "Potvrdiť";
            _submitBtn.SetEnabled(false);
            _submitBtn.clicked += () =>
            {
                _submitBtn.SetEnabled(false);
                int correctCount = 0;
                foreach (var answerBtn in _answerBtns)
                {
                    answerBtn.clickable = null;
                    answerBtn.RemoveFromClassList("active");
                    bool isCorrect = incident.ActiveQNA.CorrectAnswers.Contains(answerBtn.text);
                    bool isSelected = _selectedAnswerBtns.Contains(answerBtn);

                    if (isCorrect && isSelected)
                    {
                        answerBtn.AddToClassList("quiz-answer-correct");
                        correctCount++;
                    }
                    else if (isCorrect)
                    {
                        answerBtn.AddToClassList("quiz-answer-missed");
                    }
                    else if (isSelected)
                    {
                        answerBtn.AddToClassList("quiz-answer-wrong");
                        correctCount--;
                    }
                }
                if (correctCount == incident.ActiveQNA.CorrectAnswers.Count)
                {
                    OnQuizAnsweredCorrectly?.Invoke();
                    UpdateIncidentSymbols(true);
                }
                else
                {
                    UpdateIncidentSymbols(false);
                }
                incident.SetQuizAnswered();
            };
        }

        private void CreateIncidentSymbols(int incidentCount)
        {
            var container = _document.rootVisualElement.Q("incidents-container");
            container.Clear();
            _incidentSymbols = new List<Image>();
            for (int i = 0; i < incidentCount; i++)
            {
                _incidentSymbols.Add(Utils.Create<Image>(addTo: container, "incident-symbol"));
            }
        }

        private void UpdateIncidentSymbols(bool isCorrect)
        {
            int incidentsCount = _incidentSymbols.Count;
            if (incidentsCount > 0)
            {
                Image incidentSymbol = _incidentSymbols[incidentsCount - 1];
                _incidentSymbols.RemoveAt(incidentsCount - 1);
                if (isCorrect)
                    incidentSymbol.AddToClassList("incident-symbol-correct");
                else
                    incidentSymbol.AddToClassList("incident-symbol-wrong");
            }
        }

        private void UpdateScore(int newScore)
        {
            var scorePointsLabel = (Label)_document.rootVisualElement.Q("score-points");
            scorePointsLabel.text = newScore.ToString();
        }
    }
}
