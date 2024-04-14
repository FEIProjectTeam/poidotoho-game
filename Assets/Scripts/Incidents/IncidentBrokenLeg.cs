using System.Collections.Generic;

namespace Incidents
{
    public class IncidentBrokenLeg : IncidentBase
    {
        protected override List<QNAData> QNAs { get; set; }

        protected override void Awake()
        {
            base.Awake();

            QNAs = new List<QNAData>
            {
                new(
                    question: "Kamarátka má zlomenú nohu. Z akého poistenia dostane peniaze na liečbu?",
                    correctAnswers: new List<string> { "Zo životného poistenia." },
                    wrongAnswers: new List<string>
                    {
                        "Zo zdravotného poistenia.",
                        "Zo sociálneho poistenia.",
                    }
                ),
                new(
                    question: "Mama je v nemocnici so sestrou, lebo má zlomenú nohu. Dostaneme navyše peniaze od štátu?",
                    correctAnswers: new List<string>
                    {
                        "Dostanete peniaze od komerčnej poisťovne nie od štátu."
                    },
                    wrongAnswers: new List<string>
                    {
                        "Nedostanete nič navyše, práve naopak.",
                        "Jasné, štát vám dá veľa peňazí.",
                    }
                )
            };
        }
    }
}
