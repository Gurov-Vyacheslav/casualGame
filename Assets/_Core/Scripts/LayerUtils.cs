using UnityEngine;

namespace LearnGame
{
    public static class LayerUtils
    {
        public const string PistolTag = "BaseWeapon";

        public const string BulletLayerName = "Bullet";
        public const string EnemyLayerName = "Enemy";
        public const string PlayerLayerName = "Player";
        public const string PickUpWeaponLayerName = "PickUpWeapon";
        public const string PickUpBoosterLayerName = "PickUpBooster";

        public static readonly int BulletLayer = LayerMask.NameToLayer(BulletLayerName);
        public static readonly int PickUpWeaponLayer = LayerMask.NameToLayer(PickUpWeaponLayerName);
        public static readonly int PickUpBoosterLayer = LayerMask.NameToLayer(PickUpBoosterLayerName);
        public static readonly int EnemyLayer = LayerMask.NameToLayer(EnemyLayerName);
        public static readonly int PlayerLayer = LayerMask.NameToLayer(PlayerLayerName);

        public static readonly int CharacterMask = LayerMask.GetMask(PlayerLayerName, EnemyLayerName);
        public static readonly int PickUpWeaponMask = LayerMask.GetMask(PickUpWeaponLayerName);
        public static readonly int PickUpBoosterMask = LayerMask.GetMask(PickUpBoosterLayerName);
        public static readonly int PickUpMask = LayerMask.GetMask(PickUpBoosterLayerName, PickUpWeaponLayerName);


        public static bool IsBullet(GameObject other) => other.layer == BulletLayer;
        public static bool IsPickUp(GameObject other) => other.layer == PickUpWeaponLayer || other.layer == PickUpBoosterLayer;
        public static bool IsEnemy(GameObject other) => other.layer == EnemyLayer;
        public static bool IsPlayer(GameObject other) => other.layer == PlayerLayer;
        public static bool IsCharacter(GameObject other) => IsEnemy(other) || IsPlayer(other);
    }
}
