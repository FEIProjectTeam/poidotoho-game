using System.Collections.Generic;

namespace Incidents
{
    public class IncidentSenior : IncidentBase
    {
        protected override List<QNAData> QNAs { get; set; }

        protected override void Awake()
        {
            base.Awake();

            QNAs = new List<QNAData>
            {
                new(
                    question: "Z akého poistenia dostávajú dôchodci dôchodky?",
                    correctAnswers: new List<string> { "Zo sociálneho poistenia." },
                    wrongAnswers: new List<string>
                    {
                        "Zo zdravotného poistenia.",
                        "Zo životného poistenia.",
                    }
                ),
                new(
                    question: "Aké typy sociálnych dávok poznáte?",
                    correctAnswers: new List<string> { "Nemocenská dávka, ošetrovné, vdovské." },
                    wrongAnswers: new List<string>
                    {
                        "Sociálna dávka, životná dávka, materská dávka.",
                        "Dávka v nezamestnanosti, komerčná dávka, zdravotná dávka.",
                    }
                ),
                new(
                    question: "Pre koho je určený invalidný dôchodok?",
                    correctAnswers: new List<string>
                    {
                        "Pre človeka v prípade poklesu schopnosti vykonávať zárobkovú činnosť z dôvodu dlhodobého nepriaznivého zdravotného stavu."
                    },
                    wrongAnswers: new List<string>
                    {
                        "Len pre tých ktorí sú na vozíčku.",
                        "Pre invalidov, dôchodcov, siroty a vdovy.",
                    }
                )
            };
        }
    }
}
