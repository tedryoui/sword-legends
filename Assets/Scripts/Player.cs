using System;
using DefaultNamespace.GuiHandler;
using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Gui _gui;
        [SerializeField] private ThirdPersonController _conroller;
        
        [SerializeField] private HealthPoints _healthPoints;
        [SerializeField] private ExpiriencePoints _expiriencePoints;
        public HealthPoints GetHealthPoints => _healthPoints;
        public ExpiriencePoints GetExpiriencePoints => _expiriencePoints;

        private void Start()
        {
            GameState.OnGameStateChanges += OnGameStateChanges;
            
            GetHealthPoints.Initialize();
            GetHealthPoints.OnZero += GameOver;
            
            GetExpiriencePoints.Initialize();
            GetExpiriencePoints.OnGained += LevelUp;
            
            _gui.playerHealth.Bind(this);
            _gui.playerExp.Bind(this);
            
            _gui.weaponChoose.Show();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                _gui.pauseMenu.Show();
        }

        private void OnDestroy()
        {
            GameState.OnGameStateChanges -= OnGameStateChanges;
        }

        private void LevelUp()
        {
            _gui.weaponChoose.Show();
        }

        private void GameOver()
        {
            _gui.gameOverMenu.Show();
        }

        public void TakeDamage(float amount)
        {
            GetHealthPoints.Increase(-amount);
        }

        public void GainExpirience(float amount)
        {
            GetExpiriencePoints.Increase(amount);
        }

        private void OnGameStateChanges(GameState.GameStateMode mode)
        {
            enabled = mode == GameState.GameStateMode.Gameplay;
            _conroller.enabled = mode == GameState.GameStateMode.Gameplay;
        }
    }
}