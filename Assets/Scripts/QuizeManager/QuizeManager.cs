using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizeManager : MonoBehaviour
{
    [SerializeField] private GameObject popup;
    [SerializeField] private TextMeshProUGUI questionTMP;

    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform buttonContainer;
    [SerializeField] private Button submitButton;

    public GameObject buttonsContainer;

    private List<string> buttonsTexts;
    private List<int> correctAnswers;

    private List<GameObject> answersButtons = new List<GameObject>();

    public float buttonSpacing = 10f;

    void Start()
    {
        
    }

    private void Awake()
    {
        popup.SetActive(false);
        GameManager.Instance.openQNAPopup.AddListener(openPopup);
        submitButton.onClick.AddListener(onSubmit);
    }

    private void generateAnswers()
    {
        foreach(Transform child in buttonContainer)
        {
            if (child.gameObject.GetType() == buttonPrefab.GetType())
            {
                Destroy(child.gameObject);
            }
        }

        float currentY = 0f;
        int i = 0;
        foreach (string buttonText in buttonsTexts)
        {
            GameObject button = Instantiate(buttonPrefab, buttonContainer);
            AnswerBtnBehavior behav = button.GetComponent<AnswerBtnBehavior>();
            behav.setId(i++);
            TextMeshProUGUI buttonTextMeshPro = button.GetComponentInChildren<TextMeshProUGUI>();
            buttonTextMeshPro.text = buttonText;
            button.transform.localPosition = new Vector3(0f, currentY, 0f);
            currentY += buttonSpacing + button.GetComponent<RectTransform>().sizeDelta.y;
            answersButtons.Add(button);
        }
    }

    private void openPopup(IncidentBase incident)
    {
        Debug.Log("Open Pop up handler.");
        questionTMP.text = incident.getQuestion();
        buttonsTexts = incident.getAnswers();
        correctAnswers = incident.getCorrectAnswers();

        generateAnswers();
        popup.SetActive(true);
    }

    private void checkSelectedbuttons()
    {
        int correctAnswersCount = 0;
        int inCorrectAnswersCount = 0;
        AnswerBtnBehavior[] buttonScripts = buttonsContainer.GetComponentsInChildren<AnswerBtnBehavior>();

        foreach (var buttonScript in buttonScripts)
        {
           if (buttonScript.selected)
           {
                bool isCorrect = correctAnswers.Contains(buttonScript.id);
                buttonScript.markAnswer(isCorrect);
                if (isCorrect)
                {
                    correctAnswersCount++;
                }
                else
                {
                    inCorrectAnswersCount++;
                }
           }
           else
           {
                if (correctAnswers.Contains(buttonScript.id))
                {
                    buttonScript.markCorrectNotSelected();
                    inCorrectAnswersCount++;
                }
           }
        }

        Debug.Log(string.Format("Correct answers: {0}\nIncorrect answers: {1}.", correctAnswersCount, inCorrectAnswersCount));
    }

    private void onSubmit()
    {
        checkSelectedbuttons();
        submitButton.interactable = false;
        answersButtons.ForEach(gameObject => gameObject.GetComponent<Button>().interactable = false);            
        Debug.Log("Submit button clicked");
    }
}
 