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
                    new QuestionAndAnswers("Ak� poistenie V�m pom��e ak nab�rate svoje auto?",
                        new string[] { 
                            "�iadne poistenie nem�m",
                            "poistenie automobilu", 
                            "soci�lne poistenie" },
                        new int[] { 1 }),
                });
        }
        
    }
}


