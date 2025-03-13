using LearnGame.Timer;
using UnityEngine;

namespace LearnGame.Boosters
{
    [System.Serializable]
    public class SpeedBooster
    {
        [field:SerializeField]
        public float BoostSpeed { get; private set; } = 1f;
        
        [SerializeField]
        private float _intrvalSeconds = 1f;

        private float _currentBoostSpeedTimerSeconds { get; set; }

        public bool BoosterActive => _currentBoostSpeedTimerSeconds <= _currentBoostSpeedTimerSeconds;

        private ITimer _timer;


        public void Initialize()
        {
            _timer = GameManager.Instance.Timer;
        }
        public void UpdateTimer()
        {
            _currentBoostSpeedTimerSeconds += _timer.DeltaTime;
        }
    }
}
