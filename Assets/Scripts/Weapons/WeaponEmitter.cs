using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    [Serializable]
    public abstract class WeaponEmitter : ScriptableObject
    {
        protected Weapon _weapon;
        
        public GameObject WeaponPrefab;
        protected GameObject WeaponObject;
        protected Collider _blade;

        public int gradeLevel = 0;
        
        public abstract float Delay { get; }

        public virtual void Spawn(Transform parent, Weapon weapon)
        {
            _weapon = weapon;
            
            if (WeaponObject == null)
            {
                WeaponObject = Instantiate(WeaponPrefab, parent.position, Quaternion.identity, parent);
                WeaponObject.SetActive(false);
            }
        }
        
        public abstract IEnumerator Emit();
        
        protected virtual void Overlap(Collider blade)
        {
            var bounds = blade.bounds;
            var mask = _weapon.GetEntityLayerMask;
            var spawner = _weapon.GetSpawner;
            
            var entities = Physics.OverlapBox(bounds.center, bounds.extents, blade.transform.rotation, mask);
            foreach (var entity in entities)
            {
                var processorOfObject = spawner.GetProcessorOfObject(entity.gameObject);
                processorOfObject?.TakeDamage(1.0f);
            }
        }
    }

    [Serializable]
    public abstract class WeaponEmitterParameters
    {
        public float delay;
    }
}