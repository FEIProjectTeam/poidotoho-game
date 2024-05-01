using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class SceneManager : MonoBehaviour
    {
        public static SceneManager Instance { get; private set; }

        private readonly List<int> _levelBuildIndexes = new() { 1, 2, 3 };

        private void OnEnable()
        {
            GameManager.OnGameStateChanged += LoadNextLevel;
        }

        private void OnDisable()
        {
            GameManager.OnGameStateChanged -= LoadNextLevel;
        }

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
            if (
                _levelBuildIndexes.Contains(
                    UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
                )
            )
                GameManager.Instance.UpdateGameState(GameManager.GameState.RoamingMap);
        }

        private void LoadNextLevel(GameManager.GameState gameState)
        {
            print("LoadNextLevel");
            if (
                gameState
                is GameManager.GameState.StartPlaying
                    or GameManager.GameState.LevelFinished
            )
                StartCoroutine(LoadNextLevel());
        }

        private IEnumerator LoadNextLevel()
        {
            var nextLevelIndex =
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1;
            if (
                _levelBuildIndexes.Contains(nextLevelIndex)
                && ScoreTimeManager.Instance.RemainingTime > 0
            )
            {
                AsyncOperation asyncLoadLevel =
                    UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(nextLevelIndex);
                while (!asyncLoadLevel.isDone)
                {
                    print("Loading...");
                    yield return null;
                }
                GameManager.Instance.UpdateGameState(GameManager.GameState.LevelLoaded);
            }
            else
            {
                var scene = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Main Menu");
                scene.allowSceneActivation = true;

                GameManager.Instance.UpdateGameState(GameManager.GameState.MainMenu);
            }
        }
    }
}
