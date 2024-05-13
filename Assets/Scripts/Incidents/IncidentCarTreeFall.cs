using System.Collections.Generic;

namespace Incidents
{
    public class IncidentCarTreeFall : IncidentBase
    {
        protected override List<QNAData> QNAs { get; set; }

        protected override void Awake()
        {
            base.Awake();

            QNAs = new List<QNAData>
            {
                new(
                    question: "Z akého poistenia dostaneš peniaze ak ti padne strom na auto?",
                    correctAnswers: new List<string> { "Z Povinného zmluvného poistenia." },
                    wrongAnswers: new List<string>
                    {
                        "Zo žiadneho poistenia, auto nemám poistené.",
                        "Z cestovného poistenia.",
                    }
                )
            };
        }
    }
}
