using System;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public class ExpiriencePoints
    {
        private int _level;
        private float _crrAmount;
        [SerializeField] private float _maxAmount;
        
        public event Action<float> OnChanged;
        public event Action OnGained;

        public int GetCurrentLevel => _level;
        public float GetMaxExpiriencePoints => _maxAmount;

        public void Initialize()
        {
            _crrAmount = 0;
        }

        public void Increase(float amount)
        {
            _crrAmount += amount;

            if (_crrAmount > _maxAmount)
            {
                _crrAmount = 0;

                _maxAmount += _maxAmount;
                _level += 1;
                
                OnGained?.Invoke();
            }
            
            OnChanged?.Invoke(_crrAmount);
        }

        public void SetMapExpiriencePoints(float amount)
        {
            _maxAmount = amount;
        }
    }
}