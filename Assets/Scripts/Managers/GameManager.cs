using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public static event Action<GameState> OnGameStateChanged;
        public GameState State { get; private set; }
        public int Score { get; private set; }
        public float RemainingTime { get; private set; }

        private readonly List<int> _levelBuildIndexes = new() { 1, 2 };

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
        }

        private void Start()
        {
            // TODO: keep only during development
            if (_levelBuildIndexes.Contains(SceneManager.GetActiveScene().buildIndex))
                UpdateGameState(GameState.RoamingMap);
        }

        public enum GameState
        {
            MainMenu,
            StartPlaying,
            RoamingMap,
            DoingQuiz,
            LevelFinished
        }

        public void UpdateGameState(GameState newState)
        {
            State = newState;
            switch (newState)
            {
                case GameState.MainMenu:
                    break;
                case GameState.StartPlaying:
                    Score = 0;
                    RemainingTime = 180;
                    LoadNextLevel();
                    break;
                case GameState.RoamingMap:
                    break;
                case GameState.DoingQuiz:
                    break;
                case GameState.LevelFinished:
                    GetCurrentScoreAndTime();
                    LoadNextLevel();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
            Debug.Log(newState);
            OnGameStateChanged?.Invoke(newState);
        }

        private void GetCurrentScoreAndTime()
        {
            var scoreTimeManager = FindObjectOfType<ScoreTimeManager>();
            Score = scoreTimeManager.Score;
            RemainingTime = scoreTimeManager.RemainingTime;
        }

        private void LoadNextLevel()
        {
            var nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (_levelBuildIndexes.Contains(nextLevelIndex) && RemainingTime > 0)
            {
                var scene = SceneManager.LoadSceneAsync(nextLevelIndex);
                scene.allowSceneActivation = true;

                UpdateGameState(GameState.RoamingMap);
            }
            else
            {
                var scene = SceneManager.LoadSceneAsync("Main Menu");
                scene.allowSceneActivation = true;

                UpdateGameState(GameState.MainMenu);
            }
        }
    }
}
