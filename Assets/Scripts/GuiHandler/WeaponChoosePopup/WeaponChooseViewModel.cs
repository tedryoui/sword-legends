using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.GuiHandler.WeaponChoosePopup.ChooseFabrics;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace DefaultNamespace.GuiHandler.WeaponChoosePopup
{
    [Serializable]
    public class WeaponChooseViewModel
    {
        [SerializeField] private WeaponChooseView _view;

        [SerializeField] private List<WeaponChooseFabric> _possibleWeapons;
        private List<WeaponChooseFabric> _chosenWeapons;

        public WeaponChooseView GetView
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
            _view.onButtonClickedEvent += OnButtonClicked;

            foreach (var possibleWeapon in _possibleWeapons)
                possibleWeapon.Initialize(_view.GetWeapon);
            _chosenWeapons = new List<WeaponChooseFabric>();
        }
        
        public void Show()
        {
            GameState.SetState(GameState.GameStateMode.Pause);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            GetView.gameObject.SetActive(true);

            PrepareChooseFabrics();
            DisplayChosenFabrics();
        }

        public void Hide()
        {
            GameState.SetState(GameState.GameStateMode.Gameplay);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            
            GetView.gameObject.SetActive(false);
        }

        public void OnButtonClicked(int buttonId)
        {
            var chosenFabric = _chosenWeapons[buttonId];
            chosenFabric.LazyUpgrade();
            
            Hide();
        }
        
        private void PrepareChooseFabrics()
        {
            _chosenWeapons.Clear();
            
            Random rnd = new Random();
            _chosenWeapons = _possibleWeapons
                .Where(x => x.GetConcreteWeapon.gradeLevel < 2)
                .OrderBy(x => rnd.Next()).Take(3).ToList();
        }
        
        private void DisplayChosenFabrics()
        {
            for (int i = 0; i < 3; i++)
            {
                var parent = _view.chooseSlots[i];
                
                var name = parent.Find("Wrapper/Weapon Name").GetComponent<Text>();
                var icon = parent.Find("Wrapper/Weapon Icon").GetComponent<Image>();
                var grades = new Image[]
                {
                    parent.Find("Wrapper/Grades List/Weapon Grade 1").GetComponent<Image>(),
                    parent.Find("Wrapper/Grades List/Weapon Grade 2").GetComponent<Image>(),
                    parent.Find("Wrapper/Grades List/Weapon Grade 3").GetComponent<Image>()
                };

                if (i < _chosenWeapons.Count)
                {
                    parent.gameObject.SetActive(true);
                    
                    name.text = _chosenWeapons[i].GetWeaponName;
                    icon.sprite = _chosenWeapons[i].GetWeaponIcon;

                    foreach (var grade in grades)
                        grade.gameObject.SetActive(false);
                    
                    for(int j = 0; j < _chosenWeapons[i].GetGradeLevel; j++)
                    {
                        grades[j].gameObject.SetActive(true);
                    }
                }
                else
                {
                    parent.gameObject.SetActive(false);
                }
            }
        }

    }
}