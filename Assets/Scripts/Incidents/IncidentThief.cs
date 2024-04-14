using System.Collections.Generic;

namespace Incidents
{
    public class IncidentThief : IncidentBase
    {
        protected override List<QNAData> QNAs { get; set; }

        protected override void Awake()
        {
            base.Awake();

            QNAs = new List<QNAData>
            {
                new(
                    question: "U susedov vidíš zlodeja čo spravíš?",
                    correctAnswers: new List<string> { "Zavolám na políciu." },
                    wrongAnswers: new List<string> { "Zavolám do poisťovne.", "Zavolám na 154.", }
                ),
                new(
                    question: "Po ulici uteká zlodej s ukradnutou kabelkou, čo spravíš?",
                    correctAnswers: new List<string> { "Snažím sa ho zadržať." },
                    wrongAnswers: new List<string>
                    {
                        "Utekám spolu s ním.",
                        "Kričím chyťte zlodeja.",
                    }
                )
            };
        }
    }
}
