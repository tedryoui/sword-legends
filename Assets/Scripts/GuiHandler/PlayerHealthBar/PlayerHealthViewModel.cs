using System;
using UnityEngine;

namespace DefaultNamespace.GuiHandler.PlayerHealthBar
{
    [Serializable]
    public class PlayerHealthViewModel
    {
        [SerializeField] private PlayerHealthView _view;

        Player _cachedPlayer;
        
        public void Bind(Player player)
        {
            _cachedPlayer = player;
            
            _cachedPlayer.GetHealthPoints.OnChanged += UpdateHealthBar;
        }

        private void UpdateHealthBar(float crrAmount)
        {
            _view._healthBarFiller.fillAmount =  crrAmount / _cachedPlayer.GetHealthPoints.GetMaxHealthPoints;
        }
    }
}