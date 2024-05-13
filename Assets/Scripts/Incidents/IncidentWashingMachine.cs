using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

namespace Incidents
{
    public class IncidentWashingMachine : IncidentBase
    {
        protected override List<QNAData> QNAs { get; set; }

        protected override void Awake()
        {
            base.Awake();

            QNAs = new List<QNAData>
            {
                new(
                    question: "Ak sa vám pokazí práčka a vytopíte susedov čo spravíte?",
                    correctAnswers: new List<string>
                    {
                        "Nahlásim to mojej poisťovni nech dajú peniaze susedom."
                    },
                    wrongAnswers: new List<string>
                    {
                        "Presvedčím susedov, že to nie je moja chyba.",
                        "Vyberiem všetky svoje úspory a dám ich susedom.",
                    }
                ),
                new(
                    question: "Pokazila sa mi práčka, z čoho zaplatím jej opravu?",
                    correctAnswers: new List<string>
                    {
                        "Mám poistenie a opravu mi preplatí poisťovňa."
                    },
                    wrongAnswers: new List<string>
                    {
                        "Rovno si kúpim novú práčku.",
                        "Budem chodiť prať do potoka."
                    }
                )
            };
        }
    }
}
