using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using StarterAssets;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private ThirdPersonController _playerTransform;
        [SerializeField] private Player _player;
        public Transform GetPlayerTransform => _playerTransform.transform;
        public Player GetPlayer => _player;
        
        [SerializeField] private Transform _entityParent;
        [SerializeField] private Vector3 yAndExtends;
        [SerializeField] private EntityFabric _fabric;
        
        [SerializeField] private int _maxEntitiesAmount;
        private int _crrSpawnerEntitiesAmount;
        private HashSet<GenericEntityProcessor> _entities = new HashSet<GenericEntityProcessor>();

        [SerializeField] private float _spawnDelay;
        [SerializeField] private Vector2Int _minMaxSpawnAmount;
        
        private void Start()
        {
            GameState.OnGameStateChanges += OnGameStateChanges;

            _entities = new HashSet<GenericEntityProcessor>();
            ObjectPool.RegisterPool(_fabric.Prefab.tag, _fabric.Prefab, _maxEntitiesAmount, _entityParent);
            
            StartCoroutine(SpawnCycle());
        }

        private void OnDestroy()
        {
            ObjectPool.ReleasePool();

            GameState.OnGameStateChanges -= OnGameStateChanges;
        }

        IEnumerator SpawnCycle()
        {
            while (true)
            {
                if (GameState.CrrState == GameState.GameStateMode.Pause)
                    yield return new WaitWhile(() => GameState.CrrState == GameState.GameStateMode.Pause);
                
                int spawnAmount = UnityEngine.Random.Range(_minMaxSpawnAmount.x, _minMaxSpawnAmount.y);
                if (_crrSpawnerEntitiesAmount + spawnAmount >= _maxEntitiesAmount) yield return null;
                
                _crrSpawnerEntitiesAmount += spawnAmount;

                var bounds = GetBounds();
                for (int i = 0; i < spawnAmount; i++)
                {
                    var o = _fabric.Create(_entityParent, bounds, yAndExtends.y);
                    var genericEntityProcessor = new GenericEntityProcessor(this, o)
                    {
                        maxHealthPoints = _fabric.healthPoints,
                        followOffset = _fabric.followOffset,
                        followSpeed = _fabric.followSpeed,
                        attackDelay = _fabric.attackDelay,
                        attackDamage = _fabric.attackDamage,
                        expPrefab = _fabric.ExpPrefab
                    };
                    genericEntityProcessor.OnDestroy += Kill;

                    var c = StartCoroutine(genericEntityProcessor.Update());
                    genericEntityProcessor.updateCoroutine = c;
                    _entities.Add(genericEntityProcessor);
                }
                
                yield return new WaitForSecondsRealtime(_spawnDelay);
            }
        }

        public GenericEntityProcessor GetProcessorOfObject(GameObject gameObject)
        {
            var o = _entities.FirstOrDefault(x => x.Entity.Equals(gameObject));
            if (o != null)
                return o;

            return null;
        }

        public void Kill(GenericEntityProcessor processor)
        {
            _entities.Remove(processor);
            StopCoroutine(processor.updateCoroutine);
            
            var o = processor.Entity;
            var tag = processor.Entity.tag;
            
            ObjectPool.Return(o, tag);
            _crrSpawnerEntitiesAmount -= 1;
        }

        public void SpawnExpOrb(GameObject prefab, Vector3 position)
        {
            var o = Instantiate(prefab, position, Quaternion.identity, null);
            var exp = o.GetComponent<ExpirienceOrb>();

            exp.player = _player;
        }

        private void OnGameStateChanges(GameState.GameStateMode state)
        {
            enabled = state == GameState.GameStateMode.Gameplay;
        }

        private Bounds GetBounds()
        {
            var position = transform.position;
            position.y = yAndExtends.y;

            var extends = yAndExtends;
            extends.y = 0;

            return new Bounds(position, extends);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.magenta;

            Bounds bounds = GetBounds();
            Gizmos.DrawCube(bounds.center, bounds.size);
        }
    }
}