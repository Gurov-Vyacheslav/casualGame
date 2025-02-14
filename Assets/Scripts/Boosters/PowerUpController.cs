using UnityEngine;

namespace LearnGame.Boosters
{
    public class PowerUpController : MonoBehaviour
    {

        public SpeedBooster Booster { get; private set; }

        protected void Update()
        {
            if (Booster != null)
            {
                if (Booster._currentBoostSpeedTimerSeconds <= Booster.IntrvalSeconds)
                    Booster._currentBoostSpeedTimerSeconds += Time.deltaTime;
                else
                    Booster = null;
            }
        }
        public bool BoostInclude() => Booster != null;
        public void GetSpeedBooster(SpeedBooster speedBooster)
        {
            Booster = speedBooster;
        }
    }
}