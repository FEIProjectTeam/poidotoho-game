using System.Collections.Generic;

namespace Incidents
{
    public class IncidentCarBrokenWindow : IncidentBase
    {
        protected override List<QNAData> QNAs { get; set; }

        protected override void Awake()
        {
            base.Awake();

            QNAs = new List<QNAData>
            {
                new(
                    question: "Kameň ti rozbil čelné sklo na aute, kto ti zaplatí opravu?",
                    correctAnswers: new List<string> { "Poisťovňa mi preplatí výmenu skla." },
                    wrongAnswers: new List<string>
                    {
                        "Autoservis, však je auto v záruke.",
                        "Rodičia mi dajú peniaze.",
                    }
                ),
                new(
                    question: "Srnka mi vbehla do cesty a rozbilo sa mi okno na aute, kto mi zaplatí opravu?",
                    correctAnswers: new List<string>
                    {
                        "Mám poistenie automobilu, peniaze mi dá poisťovňa."
                    },
                    wrongAnswers: new List<string>
                    {
                        "Mám cestovné poistenie, peniaze mi dá poisťovňa.",
                        "Mám životné poistenie, peniaze mi dá poisťovňa.",
                    }
                ),
                new(
                    question: "Krúpy mi rozbili čelné sklo, kto mi zaplatí opravu?",
                    correctAnswers: new List<string> { "Opravu mi preplatí poisťovňa." },
                    wrongAnswers: new List<string>
                    {
                        "Opravu mi nikto nezaplatí.",
                        "Opravu mi preplatí servis.",
                    }
                )
            };
        }
    }
}
