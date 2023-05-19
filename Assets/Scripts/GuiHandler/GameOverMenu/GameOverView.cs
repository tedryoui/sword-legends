using System;
using UnityEngine;

namespace DefaultNamespace.GuiHandler.GameOverMenu
{
    public class GameOverView : MonoBehaviour
    {
        [HideInInspector] public bool initializeState = false;

        public event Action OnRestartClicked;
        public event Action OnQuitClicked;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) OnQuitClicked?.Invoke();
        }

        public void RestartClicked() => OnRestartClicked?.Invoke();
        public void QuitClicked() => OnQuitClicked?.Invoke();
    }
}