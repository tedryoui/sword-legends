using System;
using UnityEngine;

namespace DefaultNamespace.GuiHandler.OptionsMenu
{
    [Serializable]
    public class OptionsViewModel
    {
        [SerializeField] private OptionsView _view;
        
        public OptionsView GetView
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

            _view.OnOptionsHide += Hide;
            
            _view.OnSoundValueChanged += ChangeSound;
            _view.OnMusicValueChanged += ChangeMusic;
            _view.OnQualityChanged += ChangeQuality;
        }

        private void ChangeQuality(int obj)
        {
            QualitySettings.SetQualityLevel(obj, true);
        }

        private void ChangeMusic(float obj)
        {
            var lerp = Mathf.Lerp(-80, 20, obj);
            _view.audioMixer.SetFloat("Music Volume", lerp);
        }

        private void ChangeSound(float obj)
        {
            var lerp = Mathf.Lerp(-80, 20, obj);
            _view.audioMixer.SetFloat("Sounds Volume", lerp);
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