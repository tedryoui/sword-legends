using System;
using UnityEngine;

namespace DefaultNamespace.GuiHandler.PauseMenu
{
    [Serializable]
    public class PauseMenuViewModel
    {
        [SerializeField] private PauseMenuView _view;

        public PauseMenuView GetView
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

            _view.OnOptionsClicked += Hide;
            _view.OnResumeClicked += Hide;
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

        public void Hide()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            
            GameState.SetState(GameState.GameStateMode.Gameplay);
            GetView.gameObject.SetActive(false);
        }
    }
}