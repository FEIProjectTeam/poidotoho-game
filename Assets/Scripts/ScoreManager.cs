using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    private const string SCORE_TEXT = "Body: ";
    public static ScoreManager instance;

    public TextMeshProUGUI scoreText;
    int score = 0;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = SCORE_TEXT + score.ToString();
    }

    public void addPoint() { 
        score++;
        scoreText.text = SCORE_TEXT + score.ToString();
    }
}
