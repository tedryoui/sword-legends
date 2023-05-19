using System;
using UnityEngine;

namespace DefaultNamespace.GuiHandler.PauseMenu
{
    public class PauseMenuView : MonoBehaviour
    {
        [HideInInspector] public bool initializeState = false;

        public event Action OnOptionsClicked;
        public event Action OnResumeClicked;
        public event Action OnQuitClicked;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) OnResumeClicked?.Invoke();
        }

        public void OptionsClicked() => OnOptionsClicked?.Invoke();
        public void ResumeClicked() => OnResumeClicked?.Invoke();
        public void QuitClicked() => OnQuitClicked?.Invoke();
    }
}