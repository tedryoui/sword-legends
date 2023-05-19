using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private Transform _playerReference;
        [SerializeField] private Vector3 _followOffset;
        
        [SerializeField] private Spawner _spawner;
        [SerializeField] private  LayerMask _entitiesMask;

        [SerializeField] private List<WeaponEmitter> _emitters;

        [SerializeField] private ParticleSystem _appearParticle;

        public Spawner GetSpawner => _spawner;
        public LayerMask GetEntityLayerMask => _entitiesMask;
        
        private void Start()
        {
            GameState.OnGameStateChanges += OnGameStateChanges;
        }

        private void OnDestroy()
        {
            GameState.OnGameStateChanges -= OnGameStateChanges;
        }

        private void Update()
        {
            transform.position = _playerReference.position + _followOffset;
        }

        IEnumerator EmitCycle(WeaponEmitter emitter)
        {
            float time = 0;

            while (true)
            {
                if (GameState.CrrState == GameState.GameStateMode.Pause)
                    yield return new WaitWhile(() => GameState.CrrState == GameState.GameStateMode.Pause);
                
                if (time >= emitter.Delay)
                {
                    StartCoroutine(emitter.Emit());
                    time = 0;
                }
                
                time += Time.deltaTime;
                yield return null;
            }
        }

        public void AddConcreteWeapon(WeaponEmitter concreteWeapon)
        {
            concreteWeapon.gradeLevel = 0;
            _emitters.Add(concreteWeapon);
            concreteWeapon.Spawn(transform, this);
            StartCoroutine(EmitCycle(concreteWeapon));
        }

        public bool FindConcreteWeapon(WeaponEmitter concreteWeapon)
        {
            return _emitters.Contains(concreteWeapon);
        }

        public void ShowAppearAt(Vector3 point)
        {
            /*_appearParticle.transform.position = point;
            _appearParticle.Play();*/
        }

        private void OnGameStateChanges(GameState.GameStateMode mode)
        {
            //enabled = mode == GameState.GameStateMode.Gameplay;
        }
    }
}