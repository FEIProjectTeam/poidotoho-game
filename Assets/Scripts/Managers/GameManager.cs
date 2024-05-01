using System;
using UnityEngine;

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

        public enum GameState
        {
            MainMenu,
            StartPlaying,
            LevelLoaded,
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
                    break;
                case GameState.LevelLoaded:
                    break;
                case GameState.RoamingMap:
                    break;
                case GameState.DoingQuiz:
                    break;
                case GameState.LevelFinished:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
            OnGameStateChanged?.Invoke(newState);
            print(newState);
        }
    }
}
