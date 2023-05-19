using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class ObjectPool
    {
        public static Dictionary<string, List<GameObject>> _pools;

        public static void RegisterPool(string name, GameObject obj, int amount, Transform parent = null)
        {
            _pools ??= new Dictionary<string, List<GameObject>>();

            var list = new List<GameObject>();
            
            for (int i = 0; i < amount; i++)
            {
                var position = new Vector3(-100, -100, -100);
                var gameObject = Object.Instantiate(obj, position, Quaternion.identity, parent);
                gameObject.SetActive(false);
                
                list.Add(gameObject);                
            }
            
            _pools.Add(name, list);
        }

        public static void ReleasePool()
        {
            _pools.Clear();
        }
        
        public static GameObject Get(string name) 
        {
            _pools ??= new Dictionary<string, List<GameObject>>();
            
            if (_pools.TryGetValue(name, out var pool))
            {
                foreach (var element in pool)
                {
                    if (element.activeInHierarchy) continue;
                    
                    element.SetActive(true);
                    return element;
                }
            }

            return null;
        }

        public static void Return(GameObject gameObject, string name)
        {
            _pools ??= new Dictionary<string, List<GameObject>>();
            
            if (_pools.TryGetValue(name, out var pool)) 
            {
                if (pool.Contains(gameObject))
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}