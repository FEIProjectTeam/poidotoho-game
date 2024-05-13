using System.Collections.Generic;

namespace Incidents
{
    public class IncidentHospital : IncidentBase
    {
        protected override List<QNAData> QNAs { get; set; }

        protected override void Awake()
        {
            base.Awake();

            QNAs = new List<QNAData>
            {
                new(
                    question: "Aké tri zdravotné poisťovne máme na Slovensku?",
                    correctAnswers: new List<string>
                    {
                        "Union, Všeobecná zdravotná poisťovňa, Dôvera."
                    },
                    wrongAnswers: new List<string>
                    {
                        "Dôvera, Sociálna poisťovňa, Komerčná poisťovňa.",
                        "Životná poisťovňa, Domáca poisťovňa, Union.",
                    }
                ),
                new(
                    question: "Ktoré tvrdenie o komerčných poisťovniach je správne?",
                    correctAnswers: new List<string>
                    {
                        "Komerčné poistenie je dobrovoľné a ponúkajú ho súkromné poisťovne."
                    },
                    wrongAnswers: new List<string>
                    {
                        "Komerčné poistenie je povinné a musíme ho mať všetci.",
                        "Komerčné poistenie je vymyslené, žiadne také neexistuje.",
                    }
                )
            };
        }
    }
}
