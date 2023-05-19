using DefaultNamespace.GuiHandler.GameOverMenu;
using DefaultNamespace.GuiHandler.OptionsMenu;
using DefaultNamespace.GuiHandler.PauseMenu;
using DefaultNamespace.GuiHandler.PlayerExpBar;
using DefaultNamespace.GuiHandler.PlayerHealthBar;
using DefaultNamespace.GuiHandler.WeaponChoosePopup;
using UnityEngine;

namespace DefaultNamespace.GuiHandler
{
    public class Gui : MonoBehaviour
    {
        public PlayerHealthViewModel playerHealth;
        public PlayerExpViewModel playerExp;
        public WeaponChooseViewModel weaponChoose;
        public PauseMenuViewModel pauseMenu;
        public OptionsViewModel optionsMenu;
        public GameOverViewModel gameOverMenu;
        
        public void OpenOptions()
        {
            optionsMenu.Show();
        }
    }
}