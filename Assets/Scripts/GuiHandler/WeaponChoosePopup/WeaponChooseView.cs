using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace.GuiHandler.WeaponChoosePopup
{
    public class WeaponChooseView : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;
        [HideInInspector] public bool initializeState = false;

        public List<Transform> chooseSlots;
        public Weapon GetWeapon => _weapon;
        
        public Action<int> onButtonClickedEvent;

        public void OnButtonClicked(int buttonId)
        {
            onButtonClickedEvent?.Invoke(buttonId);
        }
    }
}