using System;
using System.Collections;
using System.Collections.Generic;
using Incidents;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizeManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questionTMP;

    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform buttonContainer;
    [SerializeField] private Button submitButton;

    public GameObject buttonsContainer;

    private List<string> buttonsTexts;
    private List<int> correctAnswers;

    private List<GameObject> answersButtons = new List<GameObject>();

    private const float BUTTON_SPACING = 10f;

    private IncidentBase incidentBase;

    private void Awake()
    {
        gameObject.SetActive(false);
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

        float currentY = 100f;
        
        int i = 0;
        foreach (string buttonText in buttonsTexts)
        {
            GameObject button = Instantiate(buttonPrefab, buttonContainer);
            AnswerBtnBehavior behav = button.GetComponent<AnswerBtnBehavior>();
            behav.setId(i++);
            TextMeshProUGUI buttonTextMeshPro = button.GetComponentInChildren<TextMeshProUGUI>();
            buttonTextMeshPro.text = buttonText;
            button.transform.localPosition = new Vector3(0f, currentY, 0f);
            currentY -= button.GetComponent<RectTransform>().sizeDelta.y + BUTTON_SPACING;
            answersButtons.Add(button);
        }
    }

    private void openPopup(IncidentBase incident)
    {
        // if (incident.getIsAnswered())
        // {
        //     submitButton.interactable = false;
        //     questionTMP.text = "Už zodpovedaná otázka.";
        //     gameObject.SetActive(true);
        //     GameManager.Instance.isPopUpOpen = true;
        //     return;
        // }

        this.incidentBase = incident;

        submitButton.interactable = true;
        // questionTMP.text = incident.getQuestion();
        // buttonsTexts = incident.getAnswers();
        // correctAnswers = incident.getCorrectAnswers();

        generateAnswers();
        gameObject.SetActive(true);
    }

    private void checkSelectedbuttons()
    {
        int correctAnswersCount = 0;
        int incorrectAnswersCount = 0;
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
                    incorrectAnswersCount++;
                }
           }
           else
           {
                if (correctAnswers.Contains(buttonScript.id))
                {
                    buttonScript.markCorrectNotSelected();
                    incorrectAnswersCount++;
                }
           }
        }

        int body = 1 + (correctAnswersCount - incorrectAnswersCount);
        if (body < 0)
        {
            body = 0;
        }
        ScoreManager.Instance.addPoints(body);
        
        Debug.Log(string.Format("Correct answers: {0}\nIncorrect answers: {1}.", correctAnswersCount, incorrectAnswersCount));
    }

    private void onSubmit()
    {
        checkSelectedbuttons();
        submitButton.interactable = false;
        // incidentBase.setAnswered();
        Debug.Log("Submit button clicked");
    }

    public void closePopUp()
    {
        gameObject.SetActive(false);
    }
}
 