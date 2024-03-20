using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public static event Action<GameState> OnGameStateChanged;
        public GameState State { get; private set; }

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
            UpdateGameState(GameState.RoamingMap);
        }

        public enum GameState
        {
            MainMenu,
            StartGameplay,
            RoamingMap,
            DoingQuiz,
        }

        public void UpdateGameState(GameState newState)
        {
            State = newState;
            switch (newState)
            {
                case GameState.MainMenu:
                    break;
                case GameState.StartGameplay:
                    break;
                case GameState.RoamingMap:
                    break;
                case GameState.DoingQuiz:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }

            OnGameStateChanged?.Invoke(newState);
        }
    }
}
