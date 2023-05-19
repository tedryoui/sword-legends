using System;
using UnityEngine;

namespace DefaultNamespace.GuiHandler.PlayerExpBar
{
    [Serializable]
    public class PlayerExpViewModel
    {
        [SerializeField] private PlayerExpView _view;
        
        Player _cachedPlayer;
        
        public void Bind(Player player)
        {
            _cachedPlayer = player;
            
            _cachedPlayer.GetExpiriencePoints.OnChanged += UpdateHealthBar;
        }

        private void UpdateHealthBar(float crrAmount)
        {
            _view._expBarFiller.fillAmount =  crrAmount / _cachedPlayer.GetExpiriencePoints.GetMaxExpiriencePoints;
        }
    }
}