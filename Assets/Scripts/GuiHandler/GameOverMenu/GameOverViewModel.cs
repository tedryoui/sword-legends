using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace.GuiHandler.GameOverMenu
{
    [Serializable]
    public class GameOverViewModel
    {
        [SerializeField] private GameOverView _view;
        
        public GameOverView GetView
        {
            get
            {
                if (_view.initializeState == false)
                    Initialize();

                return _view;
            }
        }

        private void Initialize()
        {
            _view.initializeState = true;

            _view.OnRestartClicked += Restart;
            _view.OnQuitClicked += Quit;
        }
        
        private void Quit()
        {
            Application.Quit();
        }

        public void Show()
        {
            GetView.gameObject.SetActive(true);
            GameState.SetState(GameState.GameStateMode.Pause);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        public void Restart()
        {
            SceneManager.LoadScene("Game Play Scene");
        }
    }
}