using System.Collections.Generic;

namespace Incidents
{
    public class IncidentBrokenPC : IncidentBase
    {
        protected override List<QNAData> QNAs { get; set; }

        protected override void Awake()
        {
            base.Awake();

            QNAs = new List<QNAData>
            {
                new(
                    question: "Pokazil sa ti počítač, z čoho zaplatíš opravu?",
                    correctAnswers: new List<string> { "Dostanem peniaze od poisťovne." },
                    wrongAnswers: new List<string>
                    {
                        "Starý rodičia ju zaplatia.",
                        "Sám si opravím počítač.",
                    }
                ),
                new(
                    question: "Pokazím doma služobný notebook, čo teraz?",
                    correctAnswers: new List<string>
                    {
                        "Našťastie som poistený a poisťovňa mi pomôže."
                    },
                    wrongAnswers: new List<string>
                    {
                        "Bojím sa, že dostanem výpoveď.",
                        "Pokúsim sa ho doma opraviť sám.",
                    }
                )
            };
        }
    }
}
