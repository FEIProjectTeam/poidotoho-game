using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

namespace Incidents
{
    public class IncidentCarCrash : IncidentBase
    {
        private void Start()
        {
            addQuestionAndAnswer(
                new List<QuestionAndAnswers> {
                    new QuestionAndAnswers("Aké poistenie ti pomôže ak nabúraš svoje auto?",
                        new string[] { 
                            "Žiadne poistenie nemám",
                            "Poistenie automobilu", 
                            "Sociálne poistenie" },
                        new int[] { 1 }),
                });
        }
        
    }
}


