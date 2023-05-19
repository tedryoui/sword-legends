using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    [Serializable]
    public class GenericEntityProcessor
    {
        private Spawner _spawner;
        public GameObject Entity;
        private HealthPoints _healthPoints;
        private Animator _animator;
        private Collider _collider;

        [HideInInspector] public GameObject expPrefab;
        [HideInInspector] public float maxHealthPoints;
        
        [HideInInspector] public float followOffset;
        [HideInInspector] public float followSpeed;

        [HideInInspector] public float attackDamage;
        [HideInInspector] public float attackDelay;
        private float _attackTimer;
        private float _disappearTimer;

        private bool isDead = false;

        public Action<GenericEntityProcessor> OnDestroy;
        public Coroutine updateCoroutine;

        public GenericEntityProcessor(Spawner spawner, GameObject entity)
        {
            _spawner = spawner;
            Entity = entity;

            _healthPoints = new HealthPoints();
            _healthPoints.SetMaxHealthPoints(maxHealthPoints);
            _healthPoints.Initialize();
            _healthPoints.OnZero += Kill;

            isDead = false;
            
            _animator = entity.GetComponentInChildren<Animator>();
            _collider = entity.GetComponent<Collider>();
        }

        public IEnumerator Update()
        {
            _attackTimer = attackDelay;
            _collider.enabled = true;

            while (true)
            {
                if (GameState.CrrState == GameState.GameStateMode.Pause)
                    yield return new WaitWhile(() => GameState.CrrState == GameState.GameStateMode.Pause);

                if (isDead)
                {   
                    if(_disappearTimer >= 5.0f) 
                        OnDestroy?.Invoke(this);
                    
                    _disappearTimer += Time.deltaTime;
                }
                else
                {
                    var target = _spawner.GetPlayerTransform.transform.position;
                    target.y = 0;

                    var current = Entity.transform.position;
                    current.y = 0;

                    if (Vector3.Distance(current, target) >= followOffset)
                        Move(target - current);
                    else if (_attackTimer >= attackDelay)
                        Attack();

                    Entity.transform.LookAt(_spawner.GetPlayerTransform.transform);

                    _attackTimer += Time.deltaTime;
                }
                yield return null;
            }
        }

        private void Move(Vector3 direction)
        {
            // TODO - State machine
            _animator.SetBool("isAttacking", false);
            _animator.SetBool("isDead", false);
            
            var moveDirection = direction.normalized;
            Entity.transform.position += moveDirection * (followSpeed * Time.deltaTime);
        }

        private void Attack()
        {
            // TODO - State machine
            _animator.SetBool("isAttacking", true);
            _animator.SetBool("isDead", false);
            
            _spawner.GetPlayer.TakeDamage(attackDamage);
            _attackTimer = 0;
        }

        private void Kill()
        {
            // TODO - State machine
            _animator.SetBool("isAttacking", false);
            _animator.SetBool("isDead", true);
            
            _spawner.SpawnExpOrb(expPrefab, Entity.transform.position);
            _collider.enabled = false;
            
            isDead = true;
            _disappearTimer = 0;
        }

        public void TakeDamage(float amount)
        {
            _healthPoints.Increase(-amount);
        }
    }
}