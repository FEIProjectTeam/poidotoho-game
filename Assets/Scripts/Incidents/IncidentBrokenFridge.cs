using System.Collections.Generic;

namespace Incidents
{
    public class IncidentBrokenFridge : IncidentBase
    {
        protected override List<QNAData> QNAs { get; set; }

        protected override void Awake()
        {
            base.Awake();

            QNAs = new List<QNAData>
            {
                new(
                    question: "Pokazil sa elektromotor na chladničke, za čo kúpim novú?",
                    correctAnswers: new List<string>
                    {
                        "Našťastie som poistený a dostanem peniaze od poisťovne."
                    },
                    wrongAnswers: new List<string>
                    {
                        "Musím si našetriť peniaze na novú.",
                        "Je zima takže chladničku nepotrebujem.",
                    }
                ),
                new(
                    question: "Susedom som rozbil chladničku, dostanem peniaze od poisťovne?",
                    correctAnswers: new List<string>
                    {
                        "Áno, mám pripoistenie zodpovednosti za škodu."
                    },
                    wrongAnswers: new List<string>
                    {
                        "Nie, lebo nemám pripoistenie zodpovednosti za škodu.",
                        "Áno, mám poistenie chladničky.",
                    }
                )
            };
        }
    }
}
