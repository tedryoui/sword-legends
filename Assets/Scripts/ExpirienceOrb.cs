using System;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class ExpirienceOrb : MonoBehaviour
    {
        [HideInInspector] public Player player;
        
        [SerializeField] private float gainAmount;
        [SerializeField] private LayerMask _playerMask;

        private float _collectRadius = 1.0f;
        
        private void Update()
        {
            if (OverlapPlayer())
                Collect();
        }

        private void Collect()
        {
            player.GainExpirience(gainAmount);
            
            Destroy(gameObject);
        }

        private bool OverlapPlayer()
        {
            Collider[] player = new Collider[1];
            var overlapAmount = Physics.OverlapSphereNonAlloc(transform.position, _collectRadius, player, _playerMask);

            return overlapAmount != 0;
        }
    }
}