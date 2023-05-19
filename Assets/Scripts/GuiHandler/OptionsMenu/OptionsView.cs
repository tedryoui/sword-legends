using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

namespace DefaultNamespace.GuiHandler.OptionsMenu
{
    public class OptionsView : MonoBehaviour
    {
        [HideInInspector] public bool initializeState = false;

        public AudioMixer audioMixer;

        public event Action OnOptionsHide;
        public event Action<float> OnSoundValueChanged; 
        public event Action<float> OnMusicValueChanged; 
        public event Action<int> OnQualityChanged; 

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) OnOptionsHide?.Invoke();
        }

        public void ChangeSoundValue(float amount) => OnSoundValueChanged?.Invoke(amount);
        public void ChangeMusicValue(float amount) => OnMusicValueChanged?.Invoke(amount);
        public void ChangeQuality(int tier) => OnQualityChanged?.Invoke(tier);
    }
}