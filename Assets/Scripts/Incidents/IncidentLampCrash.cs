using System.Collections.Generic;

namespace Incidents
{
    public class IncidentLampCrash : IncidentBase
    {
        protected override List<QNAData> QNAs { get; set; }

        protected override void Awake()
        {
            base.Awake();

            QNAs = new List<QNAData>
            {
                new(
                    question: "Nabúrali ste autom do lampy, z čoho zaplatíte opravu?",
                    correctAnswers: new List<string> { "Z môjho poistenia." },
                    wrongAnswers: new List<string>
                    {
                        "Utečiem rýchlo preč a nezaplatím.",
                        "Vypýtam si peniaze od rodičov."
                    }
                ),
                new(
                    question: "Ak nabúram autom do lampy čo mi povedia policajti?",
                    correctAnswers: new List<string> { "Zistia či som nepil alkohol." },
                    wrongAnswers: new List<string> { "Nič, lebo utečiem.", "Pošlú ma domov." }
                )
            };
        }
    }
}
