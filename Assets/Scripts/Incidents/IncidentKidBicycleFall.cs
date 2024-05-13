using System.Collections.Generic;

namespace Incidents
{
    public class IncidentKidBicycleFall : IncidentBase
    {
        protected override List<QNAData> QNAs { get; set; }

        protected override void Awake()
        {
            base.Awake();

            QNAs = new List<QNAData>
            {
                new(
                    question: "Pokazilo sa ti koleso na bicykli čo urobíš?",
                    correctAnswers: new List<string>
                    {
                        "Zavolám asistenčnú službu, veď mám poistenie."
                    },
                    wrongAnswers: new List<string>
                    {
                        "Zavolám do servisu nech mi ho opravia.",
                        "Rozplačem sa.",
                    }
                ),
                new(
                    question: "Stal sa ti úraz na bicykli, dá ti niekto peniaze ako bolestné?",
                    correctAnswers: new List<string> { "Áno, poisťovňa mi dá peniaze." },
                    wrongAnswers: new List<string>
                    {
                        "Nikto mi nič nedá.",
                        "Mám poistenie domácnosti, nič nedostanem.",
                    }
                )
            };
        }
    }
}
