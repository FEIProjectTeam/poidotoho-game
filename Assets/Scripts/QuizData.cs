using System;
using System.Collections.Generic;
using System.Linq;

public class QNAData
{
    public QNAData(string question, List<string> correctAnswers, List<string> wrongAnswers)
    {
        Question = question;
        CorrectAnswers = correctAnswers;
        WrongAnswers = wrongAnswers;
    }

    public string Question { get; }
    public List<string> CorrectAnswers { get; }
    public List<string> WrongAnswers { get; }

    public List<string> GetShuffledAnswers()
    {
        List<string> answers = CorrectAnswers.Concat(WrongAnswers).ToList();
        List<string> shuffledAnswers = answers.OrderBy(_ => Guid.NewGuid()).ToList();
        return shuffledAnswers;
    }

    public bool AreAnswersCorrect(List<string> answers)
    {
        return answers.All(CorrectAnswers.Contains) && answers.Count == CorrectAnswers.Count;
    }
}
