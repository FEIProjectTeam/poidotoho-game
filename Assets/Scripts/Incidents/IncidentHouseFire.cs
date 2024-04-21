using System.Collections.Generic;

namespace Incidents
{
    public class IncidentHouseFire : IncidentBase
    {
        protected override List<QNAData> QNAs { get; set; }

        protected override void Awake()
        {
            base.Awake();

            QNAs = new List<QNAData>
            {
                new(
                    question: "Ak vám zhorí váš dom, kto vám dá peniaze na nový?",
                    correctAnswers: new List<string> { "Poisťovňa, v ktorej mám dom poistený." },
                    wrongAnswers: new List<string>
                    {
                        "Zdravotná poisťovňa.",
                        "Kamaráti sa nám poskladajú."
                    }
                ),
                new(
                    question: "Zrazu vám začne horieť strecha na dome, z čoho zaplatíte opravu?",
                    correctAnswers: new List<string> { "Poisťovňa zaplatí opravu strechy." },
                    wrongAnswers: new List<string>
                    {
                        "Presťahujem sa do bytu.",
                        "Zoberiem si úver."
                    }
                )
            };
        }
    }
}
