using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DefaultNamespace
{
    [Serializable]
    public class EntityFabric
    {
        [SerializeField] private GameObject _entityPrefab;
        [SerializeField] private GameObject _expPrefab;
        public GameObject Prefab => _entityPrefab;
        public GameObject ExpPrefab => _expPrefab;

        public float healthPoints = 1.0f;
        
        public float followOffset = 1.0f;
        public float followSpeed = 3.0f;

        public float attackDamage = 1.0f;
        public float attackDelay = 2.0f;

        public GameObject Create(Transform parent, Bounds bounds, float yPlacement)
        {
            var position = new Vector3(
                x: UnityEngine.Random.Range(bounds.min.x, bounds.max.x),
                y: yPlacement,
                z: UnityEngine.Random.Range(bounds.min.z, bounds.max.z));
            
            var obj = ObjectPool.Get(_entityPrefab.tag);
            obj.transform.position = position;
            
            return obj;
        }
    }
}