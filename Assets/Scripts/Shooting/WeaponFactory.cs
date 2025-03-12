using UnityEngine;

namespace LearnGame.Shooting
{
    [CreateAssetMenu(fileName = nameof(WeaponFactory), menuName = nameof(WeaponFactory))]
    public class WeaponFactory: ScriptableObject
    {
        [SerializeField]
        private WeaponView _weaponPrefab;

        [SerializeField]
        private WeaponDescription _description;

        public WeaponView Create(Transform weaponParent)
        {
            var weapon = Instantiate(_weaponPrefab ,weaponParent);

            var model = new WeaponModel(_description);

            weapon.Initialize(model);

            return weapon;
        }
    }
}
