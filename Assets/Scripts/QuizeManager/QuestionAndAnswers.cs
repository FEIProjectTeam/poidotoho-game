using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestionAndAnswers
{
    public string question;
    public List<string> answers;
    public List<int> correctAnswers;
    
    public QuestionAndAnswers(string question, List<string> answers, List<int> correctAnswers) 
    {
        this.question = question;
        this.answers = answers;
        this.correctAnswers = correctAnswers;
    }

    public QuestionAndAnswers(string question, string[] answers, int[] correctAnswers)
    {
        this.question = question;
        this.answers = new List<string>(answers);
        this.correctAnswers = new List<int>(correctAnswers);
    }
}
