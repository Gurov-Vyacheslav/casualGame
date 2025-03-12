using UnityEngine;

namespace LearnGame.Boosters
{
    [System.Serializable]
    public class SpeedBooster
    {
        [field:SerializeField]
        public float BoostSpeed { get; private set; } = 1f;
        
        [field: SerializeField]
        public float IntrvalSeconds { get; private set; } = 1f;

        public float CurrentBoostSpeedTimerSeconds { get; set; }
    }
}
