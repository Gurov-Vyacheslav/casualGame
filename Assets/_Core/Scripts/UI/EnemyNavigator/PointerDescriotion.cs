using System;
using UnityEngine;

namespace LearnGame.UI.EnemyNavigator
{
    [Serializable]
    public class PointerDescriotion
    {
        [field: SerializeField]
        public int MaxSizePx { get; private set; } = 100;
        [field: SerializeField]
        public int MinSizePx { get; private set; } = 50;
        [field: SerializeField]
        public float MaxDistanceEffect { get; private set; } = 100f;
        [field: SerializeField]
        public float MinDistanceEffect { get; private set; } = 20f;

        [field: SerializeField]
        public float EdgeOffset { get; private set; } = 50f;
    }
}
