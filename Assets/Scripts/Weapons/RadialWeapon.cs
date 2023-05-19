using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    [CreateAssetMenu(menuName = "Weapon/Radial", fileName = "RadialWeapon", order = 0)]
    public class RadialWeapon : WeaponEmitter
    {
        [SerializeField] protected List<RadialWeaponParameters> _parameters;
        public override float Delay => _parameters[gradeLevel].delay;
        public float RotationSpeed => _parameters[gradeLevel].rotationSpeed;
        public float Duration => _parameters[gradeLevel].duration;
        
        private float _time;
        
        public override void Spawn(Transform parent, Weapon weapon)
        {
            base.Spawn(parent, weapon);

            var transform = WeaponObject.transform.Find("Blade");
            if (transform != null) _blade = transform.GetComponent<Collider>();
        }

        public override IEnumerator Emit()
        {
            _time = 0;
            WeaponObject.SetActive(true);
            _weapon.ShowAppearAt(_blade.transform.position);
            
            while (_time <= Duration)
            {
                if (GameState.CrrState == GameState.GameStateMode.Pause)
                    yield return new WaitWhile(() => GameState.CrrState == GameState.GameStateMode.Pause);
                
                WeaponObject.transform.Rotate(Vector3.up, RotationSpeed * Time.deltaTime);

                Overlap(_blade);
                
                _time += Time.deltaTime;
                yield return null;
            }
            
            WeaponObject.SetActive(false);
        }
    }

    [Serializable]
    public class RadialWeaponParameters : WeaponEmitterParameters
    {
        public float rotationSpeed;
        public float duration;
    }
}