using System.Collections.Generic;

namespace Incidents
{
    public class IncidentBrokenTV : IncidentBase
    {
        protected override List<QNAData> QNAs { get; set; }

        protected override void Awake()
        {
            base.Awake();

            QNAs = new List<QNAData>
            {
                new(
                    question: "Odrazu mi začalo dymiť z televízora, za čo si kúpim nový?",
                    correctAnswers: new List<string> { "Za peniaze z poistenia bývania." },
                    wrongAnswers: new List<string>
                    {
                        "Nekúpim, budem pozerať všetko na notebooku.",
                        "Zložia sa mi spolubývajúci."
                    }
                ),
                new(
                    question: "U kamoša som rozbil televízor, odkiaľ zoberiem peniaze na nový?",
                    correctAnswers: new List<string> { "Z poistenia zodpovednosti za škodu." },
                    wrongAnswers: new List<string>
                    {
                        "Nemusím nič platiť, určite má veľa peňazí.",
                        "Odpracujem si to uňho upratovaním."
                    }
                )
            };
        }
    }
}
