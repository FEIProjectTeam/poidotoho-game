using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class QNAData
{
    public string question;
    public List<string> correctAnswers;
    public List<string> wrongAnswers;

    public List<string> GetShuffledAnswers()
    {
        List<string> answers = correctAnswers.Concat(wrongAnswers).ToList();
        var rnd = new Random();
        answers = answers.OrderBy(_ => rnd.Next()).ToList();
        return answers;
    }

    public bool AreAnswersCorrect(List<string> answers)
    {
        return answers.All(correctAnswers.Contains) && answers.Count == correctAnswers.Count;
    }

}

[Serializable]
public class IncidentData
{
    public string name;
    public List<QNAData> qnas;

}

[Serializable]
public class QuizData
{
    public List<IncidentData> incidents;
}