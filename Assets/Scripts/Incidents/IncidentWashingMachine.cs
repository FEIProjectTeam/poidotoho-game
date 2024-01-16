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
                    new QuestionAndAnswers("Ak sa v�m pokaz� pr��ka a vytop�te susedov, �o sprav�te?",
                        new string[]
                        {
                            "nahl�sim to mojej pois�ovni nech daju peniaze susedom",
                            "presved��m susedov, �e to nie je moja chyba",
                            "vyberiem v�etky svoje �spory a d�m ich susedom"
                        },
                        new int[] {0 }),
                });
        }
    }
}