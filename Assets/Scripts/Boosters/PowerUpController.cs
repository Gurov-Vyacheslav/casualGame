﻿using LearnGame.Animations;
using UnityEngine;

namespace LearnGame.Boosters
{
    public class PowerUpController : MonoBehaviour
    {
        [SerializeField]  
        private ParticleSystem _boostParticle;

        public SpeedBooster Booster { get; private set; }

        private ICharacterPowerUpAnimationSetting _animationSetting;
        public bool BoostInclude => Booster != null;

        protected void Awake()
        {
            _animationSetting = GetComponent<ICharacterPowerUpAnimationSetting>();
        }
        protected void Update()
        {
            if (BoostInclude)
            {
                if (Booster.BoosterActive)
                {
                    Booster.UpdateTimer();
                }
                else
                {
                    Booster = null;
                    _boostParticle.Stop();
                    _animationSetting.SetBoostSpeed();
                }
            }
        }

        public void GetSpeedBooster(SpeedBooster speedBooster)
        {
            if (!BoostInclude)
                _boostParticle.Play();
            Booster = speedBooster;
            Booster.Initialize();
            _animationSetting.SetBoostSpeed(Booster.BoostSpeed);
        }
    }
}