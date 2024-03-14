using System;
using System.Collections.Generic;
using Incidents;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class LevelUI : MonoBehaviour
    {
        public static event Action QuizAnsweredCorrectly;

        [SerializeField]
        private UIDocument _document;

        [SerializeField]
        private StyleSheet _styleSheet;
        private List<Button> _answerBtns;
        private List<Button> _selectedAnswerBtns;
        private Button _submitBtn;
        private Label _scorePointsLabel;

        private void Awake()
        {
            var root = _document.rootVisualElement;
            root.Clear();
            root.styleSheets.Add(_styleSheet);

            var scoreContainer = Create(addTo: root, "score-container");
            Create<Label>(addTo: scoreContainer, "score-text").text = "body:";
            _scorePointsLabel = Create<Label>(addTo: scoreContainer, "score-points");
            _scorePointsLabel.text = "0";

            var quizContainer = Create(addTo: root, "quiz-container");
            quizContainer.name = "quiz-container";
        }

        private void OnEnable()
        {
            IncidentBase.IncidentFound += OpenQuiz;
            ScoreManager.ScoreUpdated += UpdateScore;
        }

        private void OnDisable()
        {
            IncidentBase.IncidentFound -= OpenQuiz;
            ScoreManager.ScoreUpdated -= UpdateScore;
        }

        private void OpenQuiz(IncidentBase incident)
        {
            if (GameManager.Instance.isQuizOpened)
                return;
            GameManager.Instance.isQuizOpened = true;

            var container = _document.rootVisualElement.Q("quiz-container");
            container.Clear();

            var containerBox = Create(addTo: container, "quiz-container-box");

            // Close button
            var closeBtn = Create<Button>(addTo: containerBox, "quiz-close-btn");
            closeBtn.text = "X";
            closeBtn.clicked += () =>
            {
                container.Clear();
                GameManager.Instance.isQuizOpened = false;
            };

            // Question
            var questionBox = Create(addTo: containerBox, "quiz-question-box");
            var questionLabel = Create<Label>(addTo: questionBox);
            questionLabel.text = incident.qna.question;

            // Answers
            var answerBox = Create(addTo: containerBox, "quiz-answer-box");
            _answerBtns = new List<Button>();
            _selectedAnswerBtns = new List<Button>();
            foreach (string answerText in incident.qna.GetShuffledAnswers())
            {
                var answerBtn = Create<Button>(addTo: answerBox, "quiz-answer-btn", "active");
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
            _submitBtn = Create<Button>(addTo: containerBox, "quiz-submit-btn");
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
                    bool isCorrect = incident.qna.correctAnswers.Contains(answerBtn.text);
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
                if (correctCount == incident.qna.correctAnswers.Count)
                    QuizAnsweredCorrectly?.Invoke();

                incident.SetQuizAnswered();
            };
        }

        private void UpdateScore(int newScore)
        {
            _scorePointsLabel.text = newScore.ToString();
        }

        VisualElement Create(VisualElement addTo = null, params string[] classes)
        {
            return Create<VisualElement>(addTo, classes);
        }

        T Create<T>(VisualElement addTo = null, params string[] classes)
            where T : VisualElement, new()
        {
            var ele = new T();
            addTo?.Add(ele);
            foreach (var className in classes)
            {
                ele.AddToClassList(className);
            }
            return ele;
        }
    }
}
