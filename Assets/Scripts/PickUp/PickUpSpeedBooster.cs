using UnityEngine;

namespace LearnGame.PickUp
{
    public class PickUpSpeedBooster : PickUpItem
    {
        [field: SerializeField]
        public float BoostSpeed { get; private set; } = 1f;
        [field: SerializeField]
        public float BoostSpeedIntrvalSeconds { get; private set; } = 1f;

        public override void PickUp(BaseCharacter character)
        {
            base.PickUp(character);
            character.GetBoostSpeed(this);
        }
    }
}