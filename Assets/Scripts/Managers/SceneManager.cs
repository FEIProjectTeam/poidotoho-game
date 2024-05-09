using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Managers
{
    public class SceneManager : MonoBehaviour
    {
        public static SceneManager Instance { get; private set; }

        private readonly List<int> _levelBuildIndexes = new() { 1, 2 };

        public bool IsInLastLevel()
        {
            var currIdx = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
            var lastLevelIdx = _levelBuildIndexes.Last();
            return currIdx == lastLevelIdx;
        }

        private void OnEnable()
        {
            GameManager.OnGameStateChanged += LoadNextScene;
        }

        private void OnDisable()
        {
            GameManager.OnGameStateChanged -= LoadNextScene;
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
            {
                ScoreTimeManager.Instance.ResetScoreTime();
                GameManager.Instance.UpdateGameState(GameManager.GameState.LevelLoaded);
            }
        }

        private void LoadNextScene(GameManager.GameState gameState)
        {
            var nextSceneIdx = 0;
            switch (gameState)
            {
                case GameManager.GameState.MainMenu:
                    nextSceneIdx = 0;
                    break;
                case GameManager.GameState.StartPlaying:
                    nextSceneIdx = 1;
                    break;
                case GameManager.GameState.NextLevel:
                    nextSceneIdx =
                        UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1;
                    if (
                        !_levelBuildIndexes.Contains(nextSceneIdx)
                        || ScoreTimeManager.Instance.RemainingTime == 0
                    )
                        nextSceneIdx = 0;
                    break;
            }

            switch (gameState)
            {
                case GameManager.GameState.MainMenu:
                case GameManager.GameState.StartPlaying:
                case GameManager.GameState.NextLevel:
                    StartCoroutine(LoadNextScene(nextSceneIdx));
                    break;
            }
        }

        private IEnumerator LoadNextScene(int sceneIdx)
        {
            AsyncOperation asyncLoadLevel = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(
                sceneIdx
            );
            while (!asyncLoadLevel.isDone)
            {
                yield return null;
            }
            if (sceneIdx != 0)
                GameManager.Instance.UpdateGameState(GameManager.GameState.LevelLoaded);
        }
    }
}
