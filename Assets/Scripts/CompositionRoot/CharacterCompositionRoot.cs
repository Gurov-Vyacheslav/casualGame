using LearnGame.Animations;
using LearnGame.Boosters;
using LearnGame.Movement;
using LearnGame.Shooting;
using LearnGame.Timer;
using UnityEngine;

namespace LearnGame.CompositionRoot
{
    public class CharacterCompositionRoot: CompositionRoot<BaseCharacterView>
    {
        [SerializeField]
        private CharacterConfig _characterConfig;

        private BaseCharacterView _view;

        public override BaseCharacterView Compose(ITimer timer)
        {
            _view = GetComponent<BaseCharacterView>();
            var characterAnimatorController = GetComponent<CharacterAnimatorController>();

            IMovementController movementController = new CharacterMovementController(_characterConfig, timer, GetComponent<PowerUpController>());
            IShootingTarget shootingTarget = new ShootingTargetGo(_view.gameObject);
            var shootingController = new ShootingController(shootingTarget, timer, characterAnimatorController);

            var character = new BaseCharacterModel(movementController, shootingController, _characterConfig, characterAnimatorController);
            _view.Initialize(character);

            return _view;
        }
    }
}
