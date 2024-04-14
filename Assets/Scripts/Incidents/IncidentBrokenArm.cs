using System.Collections.Generic;

namespace Incidents
{
    public class IncidentBrokenArm : IncidentBase
    {
        protected override List<QNAData> QNAs { get; set; }

        protected override void Awake()
        {
            base.Awake();

            QNAs = new List<QNAData>
            {
                new(
                    question: "Pri športe si si zlomil ruku a musíš chodiť na platené rehabilitáci. Z čoho si ich zaplatíš?",
                    correctAnswers: new List<string> { "Zo životného poistenia." },
                    wrongAnswers: new List<string>
                    {
                        "Zo sociálneho poistenia.",
                        "Z poistenia automobilu."
                    }
                ),
                new(
                    question: "Na telesnej výchove si si zlomil ruku, dostaneš nejaké peniaze ako bolestné?",
                    correctAnswers: new List<string> { "Áno, zo životného poistenia." },
                    wrongAnswers: new List<string>
                    {
                        "Zložia sa mi spolužiaci na darček.",
                        "Nie, rodičia mi nič nedajú."
                    }
                )
            };
        }
    }
}
