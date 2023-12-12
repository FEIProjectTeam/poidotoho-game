using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

namespace Incidents
{
    public class IncidentWashingMachine : IncidentBase
    {
        private void Start()
        {
            this.addQuestionAndAnswer(
                new List<QuestionAndAnswers>
                {
                    new QuestionAndAnswers("Washing question",
                        new string[]
                        {
                            "Washing answer 1",
                            "Washing answer 2",
                            "Washing answer 3",
                            "Washing answer 4",
                            "Washing answer 5"
                        },
                        new int[] { 1 }),
                });
        }
    }
}