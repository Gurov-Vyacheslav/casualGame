using System;
using UnityEngine;

namespace LearnGame.Shooting
{
    [Serializable]
    public class WeaponDescription
    {
        [field: SerializeField]
        public float ShootRadius { get; private set; } = 5f;

        [field: SerializeField]
        public float ShootFrequencySec { get; private set; } = 1f;

        [field: SerializeField]
        public BulletDescription BulletDescription { get; private set; }

        [field: SerializeField]
        public bool BaseWeapon { get; private set; } = false;
    }
}
