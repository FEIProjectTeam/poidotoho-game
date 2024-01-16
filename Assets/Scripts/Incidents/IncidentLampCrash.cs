using System.Collections.Generic;

namespace Incidents
{
    public class IncidentLampCrash : IncidentBase
    {
        private void Start()
        {
            this.addQuestionAndAnswer(
                new List<QuestionAndAnswers> {
                    new QuestionAndAnswers("Lamp crash question",
                        new string[] { "Lamp Car crash answer 1", "Lamp Car crash answer 2", "Lamp Car crash 3" },
                        new int[] { 2 }),
                });
        }
    }
}