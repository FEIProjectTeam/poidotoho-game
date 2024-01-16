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
            this.addQuestionAndAnswer(
                new List<QuestionAndAnswers> {
                    new QuestionAndAnswers("Aké poistenie Vám pomôže ak nabúrate svoje auto?",
                        new string[] { 
                            "žiadne poistenie nemám",
                            "poistenie automobilu", 
                            "sociálne poistenie" },
                        new int[] { 1 }),
                });
        }
        
    }
}


