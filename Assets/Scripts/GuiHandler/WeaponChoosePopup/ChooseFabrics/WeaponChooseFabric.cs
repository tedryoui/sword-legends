using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.GuiHandler.WeaponChoosePopup.ChooseFabrics
{
    [Serializable]
    public abstract class WeaponChooseFabric : ScriptableObject
    {
        protected Weapon _weapon;
        [SerializeField] private WeaponEmitter _concreteWeapon;

        [SerializeField] private string _weaponName;
        [SerializeField] private Sprite _weaponIcon;

        public WeaponEmitter GetConcreteWeapon => _concreteWeapon;
        public string GetWeaponName => _weaponName;
        public Sprite GetWeaponIcon => _weaponIcon;
        public int GetGradeLevel => _concreteWeapon.gradeLevel;
        
        public void Initialize(Weapon weapon)
        {
            _weapon = weapon;
            _concreteWeapon.gradeLevel = 0;
        }
        
        public void LazyUpgrade()
        {
            if (_weapon.FindConcreteWeapon(_concreteWeapon))
                Upgrade(_concreteWeapon);
            else
                Apply(_concreteWeapon);
        }

        protected abstract void Upgrade(WeaponEmitter concrete);
        protected abstract void Apply(WeaponEmitter concrete);
    }
}