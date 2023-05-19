using System;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public class HealthPoints
    {
        private float _crrAmount;
        [SerializeField] private float _maxAmount;

        public float GetMaxHealthPoints => _maxAmount;
        public event Action<float> OnChanged;
        public event Action OnZero;

        public void Initialize()
        {
            _crrAmount = _maxAmount;
        }

        public void Increase(float amount)
        {
            _crrAmount += amount;

            if (_crrAmount < 0)
            {
                _crrAmount = 0;
                
                OnZero?.Invoke();
            }

            if (_crrAmount > _maxAmount)
            {
                _maxAmount = _crrAmount;
            }
            
            OnChanged?.Invoke(_crrAmount);
        }

        public void SetMaxHealthPoints(float amount)
        {
            _maxAmount = amount;
        }
    }
}