using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class IncidentCarCrash : IncidentBase
{
    private void Start()
    {
        this.addQuestionAndAnswer(
        new List<QuestionAndAnswers> {
            new QuestionAndAnswers("Car crash question",
            new string[] { "Car crash answer 1", "Car crash answer 2", "Car crash 3" },
            new int[] { 1, 2 }),
        });
    }
}
