using System;
using System.Collections.Generic;
using System.Linq;
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
        private VisualElement _incidentsContainer;
        private VisualElement _quizContainer;
        private List<School> _schools;
        private DropdownField _schoolDropdownField;

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

            _incidentsContainer = Utils.Create(addTo: root, "incidents-container");
            _incidentsContainer.name = "incidents-container";

            var timerContainer = Utils.Create(addTo: root, "timer-container");
            Utils.Create<Label>(addTo: timerContainer, "timer-text").text = "čas:";
            var timerValueLabel = Utils.Create<Label>(addTo: timerContainer, "timer-value");
            timerValueLabel.name = "timer-value";
            timerValueLabel.text = "∞";

            var controlsContainer = Utils.Create(addTo: root, "controls-container");
            var soundControl = Utils.Create<Image>(addTo: controlsContainer);
            soundControl.name = "sound-control";
            soundControl.AddManipulator(new Clickable(evt => AudioManager.Instance.ToggleMute()));
            UpdateControls();

            _quizContainer = Utils.Create(addTo: root, "quiz-container");
            _quizContainer.name = "quiz-container";
        }

        private void OnEnable()
        {
            IncidentBase.OnIncidentFound += OpenQuiz;
            ScoreTimeManager.OnScoreUpdated += UpdateScore;
            IncidentManager.OnIncidentsSpawned += CreateIncidentSymbols;
            GameManager.OnGameStateChanged += OpenLevelSummary;
            AudioManager.OnAudioToggleMute += UpdateControls;
        }

        private void OnDisable()
        {
            IncidentBase.OnIncidentFound -= OpenQuiz;
            ScoreTimeManager.OnScoreUpdated -= UpdateScore;
            IncidentManager.OnIncidentsSpawned -= CreateIncidentSymbols;
            GameManager.OnGameStateChanged -= OpenLevelSummary;
            AudioManager.OnAudioToggleMute -= UpdateControls;
        }

        private void OpenQuiz(IncidentBase incident)
        {
            if (GameManager.Instance.State == GameManager.GameState.DoingQuiz)
                return;
            GameManager.Instance.UpdateGameState(GameManager.GameState.DoingQuiz);

            _quizContainer.Clear();

            var containerBox = Utils.Create(addTo: _quizContainer, "quiz-container-box");

            // Close button
            var closeBtn = Utils.Create<Button>(addTo: containerBox, "quiz-close-btn");
            closeBtn.text = "X";
            closeBtn.clicked += () =>
            {
                _quizContainer.Clear();
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
                OnQuizAnsweredCorrectly?.Invoke();
                incident.SetQuizAnswered();
            };
        }

        private void CreateIncidentSymbols(int incidentCount)
        {
            _incidentsContainer.Clear();
            _incidentSymbols = new List<Image>();
            for (int i = 0; i < incidentCount; i++)
            {
                _incidentSymbols.Add(
                    Utils.Create<Image>(addTo: _incidentsContainer, "incident-symbol")
                );
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

        private void UpdateControls()
        {
            var soundControl = (Image)_document.rootVisualElement.Q("sound-control");
            soundControl.ClearClassList();
            soundControl.AddToClassList(
                AudioManager.Instance.IsAudioMuted() ? "sound-control-off" : "sound-control-on"
            );
        }

        private void OpenLevelSummary(GameManager.GameState gameState)
        {
            if (gameState != GameManager.GameState.LevelFinished)
                return;

            var remainingTime = ScoreTimeManager.Instance.RemainingTime;
            var isInLastLevel = SceneManager.Instance.IsInLastLevel();
            var root = _document.rootVisualElement;
            root.Clear();

            var summaryContainer = Utils.Create(addTo: root, "summary-container");
            var containerBox = Utils.Create(addTo: summaryContainer, "summary-container-box");

            var topBox = Utils.Create(addTo: containerBox, "summary-box");
            var topBoxLabel = Utils.Create<Label>(
                addTo: topBox,
                "w-full",
                "text-middle-center",
                "whitespace-normal"
            );
            if (remainingTime > 0)
            {
                topBoxLabel.text = isInLastLevel
                    ? "Dostal si sa na koniec hry a našiel si všetky poistné udalosti, si super!"
                    : "V tomto leveli sa ti podarilo nájsť všetky poistné udalosti, len tak ďalej!";

                var middleBox = Utils.Create(addTo: containerBox, "summary-box");
                Utils.Create<Label>(addTo: middleBox, "summary-text").text = "Zostávajúci čas:";
                Utils.Create<Label>(addTo: middleBox, "summary-value").text =
                    $"{ScoreTimeManager.FormatTime(ScoreTimeManager.Instance.RemainingTime)}";
            }
            else
            {
                topBoxLabel.text =
                    "Kým si hľadal poistné udalosti tak ti uplynul všetok čas... Nevadí, skús to znovu!";
            }

            var bottomBox = Utils.Create(addTo: containerBox, "summary-box");
            Utils.Create<Label>(addTo: bottomBox, "summary-text").text = "Získané body:";
            Utils.Create<Label>(addTo: bottomBox, "summary-value").text =
                $"{ScoreTimeManager.Instance.Score}";

            var btnBox = Utils.Create(addTo: containerBox, "summary-box");

            var quitBtn = Utils.Create<Button>(addTo: btnBox, "submit-btn");
            quitBtn.text = "Ukončiť hru";
            quitBtn.clicked += OpenSubmitDataForm;

            if (remainingTime == 0 || isInLastLevel)
                return;

            var continueBtn = Utils.Create<Button>(addTo: btnBox, "submit-btn");
            continueBtn.text = "Pokračovať";
            continueBtn.clicked += () =>
            {
                GameManager.Instance.UpdateGameState(GameManager.GameState.NextLevel);
            };
        }

        private void OpenSubmitDataForm()
        {
            var root = _document.rootVisualElement;
            root.Clear();

            var submitContainer = Utils.Create(addTo: root, "submit-container");
            var containerBox = Utils.Create(addTo: submitContainer, "submit-container-box");

            var topBox = Utils.Create(addTo: containerBox, "w-full", "flex-row");
            var topBoxLabel = Utils.Create<Label>(
                addTo: topBox,
                "w-full",
                "text-middle-center",
                "whitespace-normal",
                "m-0",
                "p-0"
            );
            topBoxLabel.text = "Zapoj sa do súťaže a ukáž všetkým aký si dobrý!";

            var middleBox = Utils.Create(
                addTo: containerBox,
                "flex-row",
                "w-full",
                "justify-around"
            );

            var middleLeftBox = Utils.Create(addTo: middleBox, "flex-col", "w-80pe");
            var nicknameLabel = Utils.Create<Label>(addTo: middleLeftBox);
            nicknameLabel.text = "Prezývka:";
            var nicknameTextField = Utils.Create<TextField>(
                addTo: middleLeftBox,
                "submit-input-field"
            );
            nicknameTextField.maxLength = 32;

            var middleRightBox = Utils.Create(addTo: middleBox, "flex-col", "w-20pe");
            var gradeLabel = Utils.Create<Label>(addTo: middleRightBox);
            gradeLabel.text = "Trieda:";
            var gradeTextField = Utils.Create<TextField>(
                addTo: middleRightBox,
                "submit-input-field"
            );
            gradeTextField.maxLength = 6;

            var schoolBox = Utils.Create(addTo: containerBox, "flex-col", "w-full");
            var schoolLabel = Utils.Create<Label>(addTo: schoolBox);
            schoolLabel.text = "Nájdi svoju školu:";

            var schoolSearchBox = Utils.Create(
                addTo: schoolBox,
                "flex-row",
                "w-full",
                "justify-around"
            );
            var schoolSearchField = Utils.Create<TextField>(
                addTo: schoolSearchBox,
                "school-search-field",
                "w-80pe"
            );
            schoolSearchField.maxLength = 40;
            var schoolSearchBtn = Utils.Create<Button>(
                addTo: schoolSearchBox,
                "school-search-btn",
                "w-20pe"
            );
            schoolSearchBtn.text = "Hľadať";

            var schoolDropdownField = Utils.Create<DropdownField>(
                addTo: schoolBox,
                "w-full",
                "school-dropdown"
            );
            schoolSearchBtn.clicked += () => SearchForSchools(schoolSearchField.value);

            var btnBox = Utils.Create(addTo: containerBox, "w-full", "flex-row");

            var quitBtn = Utils.Create<Button>(addTo: btnBox, "submit-btn");
            quitBtn.text = "Preskočiť";
            quitBtn.clicked += () =>
            {
                GameManager.Instance.UpdateGameState(GameManager.GameState.MainMenu);
            };

            var submitBtn = Utils.Create<Button>(addTo: btnBox, "submit-btn");
            submitBtn.text = "Zapojiť sa";
            submitBtn.SetEnabled(false);
            submitBtn.clicked += () =>
            {
                var schoolId = GetSchoolId(schoolDropdownField.value);
                if (schoolId == -1)
                    return;
                StartCoroutine(
                    NetworkManager.SubmitGameSessionData(
                        nicknameTextField.value,
                        gradeTextField.value,
                        schoolId,
                        ScoreTimeManager.Instance.Score,
                        (int)ScoreTimeManager.Instance.RemainingTime
                    )
                );
            };

            nicknameTextField.RegisterValueChangedCallback(_ => ValidateInputFields());
            gradeTextField.RegisterValueChangedCallback(_ => ValidateInputFields());
            schoolDropdownField.RegisterValueChangedCallback(_ => ValidateInputFields());
            return;

            void ValidateInputFields()
            {
                if (
                    string.IsNullOrEmpty(nicknameTextField.value)
                    || string.IsNullOrEmpty(gradeTextField.value)
                    || string.IsNullOrEmpty(schoolDropdownField.value)
                )
                    submitBtn.SetEnabled(false);
                else
                    submitBtn.SetEnabled(true);
            }

            int GetSchoolId(string schoolName)
            {
                var school = _schools.FirstOrDefault(s => s.name == schoolName);
                if (school == null)
                    return -1;
                return school.id;
            }

            void UpdateSchoolDropdown(School[] schools)
            {
                _schools = schools.ToList();
                schoolDropdownField.choices = schools.Select(s => s.name).ToList();
            }

            void SearchForSchools(string schoolName)
            {
                if (string.IsNullOrEmpty(schoolName))
                    return;

                StartCoroutine(NetworkManager.FilterSchools(schoolName, UpdateSchoolDropdown));
            }
        }
    }
}
