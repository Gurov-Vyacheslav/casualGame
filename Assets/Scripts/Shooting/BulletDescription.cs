using System;
using UnityEngine;

namespace LearnGame.Shooting
{
    [Serializable]
    public class BulletDescription
    {
        [field: SerializeField]
        public float Damage { get; private set; } = 1f;

        [field: SerializeField]
        public float BulletMaxFlyDistance { get; private set; } = 10f;

        [field: SerializeField]
        public float BulletFlySpeed { get; private set; } = 10f;
    }
}
