using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    [CreateAssetMenu(menuName = "Weapon/Assassin", fileName = "AssassinWeapon", order = 2)]
    public class AssassinWeapon : WeaponEmitter
    {
        [SerializeField] protected List<AssassinWeaponParameters> _parameters;
        public override float Delay => _parameters[gradeLevel].delay;
        public int BladesAmount => _parameters[gradeLevel].bladesAmount;
        public float ThrowSpeed => _parameters[gradeLevel].throwSpeed;
        public float ThrowOffset;

        public override void Spawn(Transform parent, Weapon weapon)
        {
            _weapon = weapon;
            
            WeaponObject = new GameObject("Assassin Blades");
            WeaponObject.transform.parent = weapon.transform;
            
            ObjectPool.RegisterPool("assassinBlades", WeaponPrefab, 20, WeaponObject.transform);
        }
        
        public override IEnumerator Emit()
        {
            if(!PlaceBlades(out var blades, out var bladesColliders)) yield break;
            
            var time = 0.0f;
            while (time <= Delay)
            {
                if (GameState.CrrState == GameState.GameStateMode.Pause)
                    yield return new WaitWhile(() => GameState.CrrState == GameState.GameStateMode.Pause);
                
                foreach (var blade in blades)
                    blade.transform.position += blade.transform.forward * (ThrowSpeed * Time.deltaTime);

                foreach (var collider in bladesColliders)
                    Overlap(collider);

                time += Time.deltaTime;
                yield return null;
            }

            foreach (var blade in blades)
                ObjectPool.Return(blade, "assassinBlades");
        }

        private bool PlaceBlades(out List<GameObject> blades, out List<Collider> bladesColliders)
        {
            var rndOffset = UnityEngine.Random.Range(0, 90);
            var step = 360.0f / BladesAmount;
            blades = new List<GameObject>();
            bladesColliders = new List<Collider>();
            
            for (int i = 0; i < BladesAmount; i++)
            {
                var blade = ObjectPool.Get("assassinBlades");
                if (blade == null) return false;
                
                blade.transform.position = _weapon.transform.position;
                blade.transform.rotation = Quaternion.identity;
            
                Quaternion rotation = Quaternion.Euler(0, step * i + rndOffset, 0);
                blade.transform.rotation = rotation;

                var offset = blade.transform.forward * ThrowOffset;
                blade.transform.position += offset;
                
                blades.Add(blade);
                bladesColliders.Add(blade.GetComponent<Collider>());
            }

            return true;
        }
    }

    [Serializable]
    public class AssassinWeaponParameters : WeaponEmitterParameters
    {
        public int bladesAmount;
        public float throwSpeed;
    }
}