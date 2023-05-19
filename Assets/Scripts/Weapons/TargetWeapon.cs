using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    [CreateAssetMenu(menuName = "Weapon/Target", fileName = "TargetWeapon", order = 1)]
    public class TargetWeapon : WeaponEmitter
    {
        [SerializeField] protected List<TargetWeaponParameters> _parameters;
        public override float Delay => _parameters[gradeLevel].delay;
        public float ThrowDistance => _parameters[gradeLevel].throwDistance;
        public float ThrowSpeed => _parameters[gradeLevel].throwSpeed;

        public override void Spawn(Transform parent, Weapon weapon)
        {
            _weapon = weapon;
            
            WeaponObject = new GameObject("Target Blades");
            WeaponObject.transform.parent = weapon.transform;
            
            ObjectPool.RegisterPool("targetBlades", WeaponPrefab, 20, WeaponObject.transform);
        }

        public override IEnumerator Emit()
        {
            var blade = ObjectPool.Get("targetBlades");
            if (blade == null) yield break;
            var bladeCollider = blade.GetComponent<Collider>();

            if (CatchTarget(out var target))
                PlaceBlade(blade, target);

            _weapon.ShowAppearAt(blade.transform.position + blade.transform.forward);
            
            var time = 0.0f;
            while (time <= Delay)
            {
                if (GameState.CrrState == GameState.GameStateMode.Pause)
                    yield return new WaitWhile(() => GameState.CrrState == GameState.GameStateMode.Pause);
                
                blade.transform.position += blade.transform.forward * (ThrowSpeed * Time.deltaTime);
                Overlap(bladeCollider);

                time += Time.deltaTime;
                yield return null;
            }

            ObjectPool.Return(blade, "targetBlades");
        }

        private bool CatchTarget(out Vector3 target)
        {
            target = Vector3.zero;

            var point = _weapon.transform.position;
            var mask = _weapon.GetEntityLayerMask;
            var spawner = _weapon.GetSpawner;
            
            var entities = Physics.OverlapSphere(point, ThrowDistance, mask);
            if (entities.Length == 0) return false;

            var collider = entities.OrderBy(x => Vector3.Distance(x.transform.position, _weapon.transform.position)).First();
            target = collider.transform.position;
            
            return true;
        }

        private void PlaceBlade(GameObject blade, Vector3 target)
        {
            blade.transform.position = _weapon.transform.position;
            blade.transform.rotation = Quaternion.identity;

            target.y = blade.transform.position.y;
            blade.transform.LookAt(target);
        }
    }
    
    [Serializable]
    public class TargetWeaponParameters : WeaponEmitterParameters
    {
        public float throwDistance;
        public float throwSpeed;
    }
}