using UnityEngine;

namespace LearnGame.Boosters
{
    public class PowerUpController : MonoBehaviour
    {
        [SerializeField] 
        private ParticleSystem _boostParticle;

        public SpeedBooster Booster { get; private set; }

        protected void Update()
        {
            if (Booster != null)
            {
                if (Booster._currentBoostSpeedTimerSeconds <= Booster.IntrvalSeconds)
                {
                    Booster._currentBoostSpeedTimerSeconds += Time.deltaTime;
                }
                else
                {
                    Booster = null;
                    _boostParticle.Stop();
                }
            }
        }
        public bool BoostInclude() => Booster != null;

        public void GetSpeedBooster(SpeedBooster speedBooster)
        {
            if (!BoostInclude())
                _boostParticle.Play();
            Booster = speedBooster;
        }
    }
}