using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    private const string SCORE_TEXT = "Body: ";
    public static ScoreManager Instance;

    public TextMeshProUGUI scoreText;
    int score = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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
    public void addPoints(int score)
    {
        this.score += score;
        scoreText.text = SCORE_TEXT + this.score;
    }
}
