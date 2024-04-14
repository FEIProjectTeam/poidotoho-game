using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

namespace Incidents
{
    public class IncidentCarCrash : IncidentBase
    {
        protected override List<QNAData> QNAs { get; set; }

        protected override void Awake()
        {
            base.Awake();

            QNAs = new List<QNAData>
            {
                new(
                    question: "Aké poistenie vám pomôže ak raz nabúrate svoje auto?",
                    correctAnswers: new List<string> { "Poistenie automobilu." },
                    wrongAnswers: new List<string>
                    {
                        "Žiadne poistenie nepomôže.",
                        "Sociálne poistenie."
                    }
                ),
                new(
                    question: "Kam zavolám ak potrebujem odtiahnuť auto?",
                    correctAnswers: new List<string>
                    {
                        "Na číslo asistenčnej služby, som poistený."
                    },
                    wrongAnswers: new List<string>
                    {
                        "Kamarátom.",
                        "Na prvé číslo, ktoré nájdem na internete."
                    }
                )
            };
        }
    }
}
