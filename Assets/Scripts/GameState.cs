using System;

namespace DefaultNamespace
{
    public class GameState
    {
        public enum GameStateMode { Gameplay, Pause }

        public GameState(GameStateMode mode) => _crrState = mode;

        private static GameState _instance;

        private static GameState GetInstance => _instance ??= new GameState(GameStateMode.Gameplay);

        private GameStateMode _crrState;
        public static GameStateMode CrrState => GetInstance._crrState;

        public static event Action<GameStateMode> OnGameStateChanges;

        public static void SetState(GameStateMode state)
        {
            GetInstance._crrState = state;
            
            OnGameStateChanges?.Invoke(_instance._crrState);
        }
    }
}