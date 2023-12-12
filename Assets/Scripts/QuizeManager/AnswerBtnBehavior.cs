using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerBtnBehavior : MonoBehaviour
{
    public int id;
    private Color normalColor = new Color(255, 255, 255, 0);
    private Color clickedColor = Color.grey;
    private Color wrontgColor = Color.red;
    private Color correctColor = Color.green;
    private Color correctNotSelectedColor = Color.cyan;
    public bool selectMode = true;
    public bool selected = false;

    Button answerButton;
    Image buttonImage;

    private void Start()
    {
        buttonImage = this.GetComponent<Image>();
        answerButton = this.GetComponent<Button>();
    }

    private void switchSelectedButton()
    {
        if (!selected)
        {
            buttonImage.color = clickedColor;
        }
        else
        {
            buttonImage.color = normalColor;
        }
        selected = !selected;
    }


    public void OnMouseDown()
    {
        if (buttonImage == null)
        {
            return;
        }
        switchSelectedButton(); 
    }

    public void setId(int id)
    {
        this.id = id;
    }

    public void markCorrectNotSelected()
    {
        buttonImage.color = correctNotSelectedColor;
    }

    public void markAnswer(bool isCorrect)
    {
        if (isCorrect)
        {
            buttonImage.color = correctColor;            
        }   
        else
        {
            buttonImage.color = wrontgColor;
        }
    }

    public void setSelectMode(bool value)
    {
        selectMode = value;
        this.answerButton.interactable = selectMode;
    }
}
