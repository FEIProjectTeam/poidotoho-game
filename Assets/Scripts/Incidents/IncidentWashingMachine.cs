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
            addQuestionAndAnswer(
                new List<QuestionAndAnswers>
                {
                    new QuestionAndAnswers("Ak sa ti doma pokazí pračka a vytopíte susedov, čo spravíš?",
                        new string[]
                        {
                            "Nahlásim to mojej poisťovni nech dajú peniaze susedom",
                            "Presvedčím susedov, že to nie je moja chyba",
                            "Vyberiem všetky svoje úspory a dám ich susedom"
                        },
                        new int[] {0 }),
                });
        }
    }
}