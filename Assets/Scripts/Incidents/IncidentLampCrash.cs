using System.Collections.Generic;

namespace Incidents
{
    public class IncidentLampCrash : IncidentBase
    {
        private void Start()
        {
            this.addQuestionAndAnswer(
                new List<QuestionAndAnswers> {
                    new QuestionAndAnswers("Nabúrali ste autom do lampy. Z čoho zaplatíte opravu?",
                        new string[] { 
                            "z môjho poistenia", 
                            "utečiem rýchlo preč a nezaplatím", 
                            "vypýtam si peniaze od rodičov" 
                        },
                        new int[] { 0 }),
                });
        }
    }
}