using LearnGame.Boosters;
using UnityEngine;

namespace LearnGame.PickUp
{
    public class PickUpSpeedBooster : PickUpItem
    {
        [SerializeField]
        private SpeedBooster speedBooster;

        public override void PickUp(BaseCharacter character)
        {
            base.PickUp(character);
            character.GetBoostSpeed(speedBooster);
        }
    }
}