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
                    new QuestionAndAnswers("Ak sa v·m pokazÌ pr·Ëka a vytopÌte susedov, Ëo spravÌte?",
                        new string[]
                        {
                            "nahl·sim to mojej poisùovni nech daju peniaze susedom",
                            "presvedËÌm susedov, ûe to nie je moja chyba",
                            "vyberiem vöetky svoje ˙spory a d·m ich susedom"
                        },
                        new int[] {0 }),
                });
        }
    }
}