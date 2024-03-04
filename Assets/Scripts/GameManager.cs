using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Incidents;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Score
    private static string SCORE_TEXT = "Body: ";
    private int score = 0;
    [SerializeField]
    private TextMeshProUGUI scoreText;

    // Events
    public UnityEvent<IncidentBase> openQNAPopup = new UnityEvent<IncidentBase>();
    public bool isPopUpOpen = false;

    public void Awake()
    {
        this.scoreText.text = SCORE_TEXT + this.score.ToString();
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

    private void Start()
    {
        
    }

    public void LoadScene(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = true;
    }

    public void addPoint()
    {
        score++;
        scoreText.text = SCORE_TEXT + score.ToString();
    }
    public void addPoints(int score)
    {
        this.score += score;
        scoreText.text = SCORE_TEXT + this.score;
    }
}
