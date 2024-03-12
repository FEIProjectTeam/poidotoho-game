using System.Collections.Generic;
using Incidents;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class LevelUI : MonoBehaviour
    {
        [SerializeField] private UIDocument _document;
        [SerializeField] private StyleSheet _styleSheet;
        private List<string> _selectedAnswers;
        
        private void Start()
        {
            // StartCoroutine(Generate());
        }

        private void OnEnable()
        {
            IncidentBase.IncidentFound += OpenQuiz;
        }
        
        private void OnDisable()
        {
            IncidentBase.IncidentFound -= OpenQuiz;
        }

        private void OnValidate()
        {
            // if (Application.isPlaying) return;
            // StartCoroutine(Generate());
        }

        private void OpenQuiz(IncidentBase incident)
        // private IEnumerator Generate()
        {
            if (GameManager.Instance.isQuizOpened)
                return;
            GameManager.Instance.isQuizOpened = true;
            // yield return null;
            var root = _document.rootVisualElement;
            root.Clear();
            root.styleSheets.Add(_styleSheet);
            _selectedAnswers = new List<string>();

            var container = Create("container");

            // Close button
            var closeBtn = Create<Button>("close-btn");
            var closeBtnLabel = new Label();
            closeBtnLabel.text = "X";
            closeBtn.Add(closeBtnLabel);
            closeBtn.clicked += () =>
            {
                root.Clear();
                GameManager.Instance.isQuizOpened = false;
            };
            

            // Question field
            var quizQuestionBox = Create("quiz-question-box");           
            var quizQuestionLabel = Create<Label>();
            quizQuestionLabel.text = incident.qna.question;
            quizQuestionBox.Add(quizQuestionLabel);

            // Answers field
            var quizAnswerBox = Create("quiz-answer-box");
            container.Add(quizAnswerBox);
            quizAnswerBox.Add(closeBtn);
            quizAnswerBox.Add(quizQuestionBox);

            var quizAnswerBtnBox = Create("quiz-answer-btn-box");
            quizAnswerBox.Add(quizAnswerBtnBox);

            foreach (string answer in incident.qna.GetRandomizedAnswers())
            {
                var quizAnswerBtn = Create<Button>("quiz-answer-btn");
                quizAnswerBtn.text = answer;
                quizAnswerBtn.clicked += () =>
                {
                    if (_selectedAnswers.Contains(answer))
                    {
                        _selectedAnswers.Remove(answer);
                        quizAnswerBtn.RemoveFromClassList("quiz-answer-selected");
                    }
                    else{
                        _selectedAnswers.Add(answer);
                        quizAnswerBtn.AddToClassList("quiz-answer-selected");
                    }
                };
                quizAnswerBtnBox.Add(quizAnswerBtn);
            }

            var quizSubmitBtn = Create<Button>("quiz-submit-btn");
            quizSubmitBtn.text = "Potvrdiť odpovede";
            quizSubmitBtn.clicked += () =>
            {
                if (incident.qna.AreAnswersCorrect(_selectedAnswers)) 
                    Debug.Log("All answers are correct");
                else 
                    Debug.Log("Answers are not correct");
                incident.SetQuizAnswered();
                GameManager.Instance.isQuizOpened = false;
                root.Clear();
            };
            quizAnswerBox.Add(quizSubmitBtn);
            


            root.Add(container);
        }

        VisualElement Create(params string[] classNames)
        {
            return Create<VisualElement>(classNames);
        }

        T Create<T>(params string[] classNames) where T : VisualElement, new()
        {
            var ele = new T();
            foreach (var className in classNames)
            {
                ele.AddToClassList(className);
            }
            return ele;
        }
    }
}
