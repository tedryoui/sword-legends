using UnityEngine;

namespace DefaultNamespace.GuiHandler.WeaponChoosePopup.ChooseFabrics
{
    [CreateAssetMenu(menuName = "Choose Fabric/Radial", fileName = "Radial Choose Fabric", order = 1)]
    public class RadialChooseFabric : WeaponChooseFabric
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