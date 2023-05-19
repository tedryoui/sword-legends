using UnityEngine;

namespace DefaultNamespace.GuiHandler.WeaponChoosePopup.ChooseFabrics
{
    [CreateAssetMenu(menuName = "Choose Fabric/Assassin", fileName = "Assassin Choose Fabric", order = 2)]
    public class AssassinChooseFabric : WeaponChooseFabric
    {
        protected override void Upgrade(WeaponEmitter concrete)
        {
            concrete.gradeLevel += 1;
        }

        protected override void Apply(WeaponEmitter concrete)
        {
            _weapon.AddConcreteWeapon(concrete);
            concrete.gradeLevel += 1;
        }
    }
}