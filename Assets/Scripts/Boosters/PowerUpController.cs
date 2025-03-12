using LearnGame.Animations;
using UnityEngine;

namespace LearnGame.Boosters
{
    public class PowerUpController : MonoBehaviour
    {
        [SerializeField] 
        private ParticleSystem _boostParticle;

        public SpeedBooster Booster { get; private set; }

        private ICharacterPowerUpAnimationSetting _animationSetting;

        protected void Awake()
        {
            _animationSetting = GetComponent<ICharacterPowerUpAnimationSetting>();
        }
        protected void Update()
        {
            if (Booster != null)
            {
                if (Booster.CurrentBoostSpeedTimerSeconds <= Booster.IntrvalSeconds)
                {
                    Booster.CurrentBoostSpeedTimerSeconds += Time.deltaTime;
                }
                else
                {
                    Booster = null;
                    _boostParticle.Stop();
                    _animationSetting.SetBoostSpeed();
                }
            }
        }
        public bool BoostInclude() => Booster != null;

        public void GetSpeedBooster(SpeedBooster speedBooster)
        {
            if (!BoostInclude())
                _boostParticle.Play();
            Booster = speedBooster;
            _animationSetting.SetBoostSpeed(Booster.BoostSpeed);
        }
    }
}