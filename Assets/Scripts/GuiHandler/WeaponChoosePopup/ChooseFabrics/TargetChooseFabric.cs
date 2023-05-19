using UnityEngine;

namespace DefaultNamespace.GuiHandler.WeaponChoosePopup.ChooseFabrics
{
    [CreateAssetMenu(menuName = "Choose Fabric/Target", fileName = "Target Choose Fabric", order = 0)]
    public class TargetChooseFabric : WeaponChooseFabric
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