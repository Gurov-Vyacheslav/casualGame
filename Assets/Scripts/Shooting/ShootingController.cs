using LearnGame.Timer;
using LearnGame.Animations;
using UnityEngine;

namespace LearnGame.Shooting
{
    public class ShootingController
    {
        public bool HasTarget => _target != null;
        public Vector3 TargetPosition => _target.Transform.Position;

        private WeaponModel _weapon;

        private readonly IShootingTarget _shootingTarget;
        private readonly ITimer _timer;
        private readonly ICharacterShootingAnimationSettings _animationSettings;

        private BaseCharacterModel _target;
        private float _nextShootTimerSec;

        public ShootingController(IShootingTarget shootingTarget, ITimer timer, ICharacterShootingAnimationSettings animationSettings)
        {
            _shootingTarget = shootingTarget;
            _timer = timer;
            _animationSettings = animationSettings;
        }

        public void TryShoot(Vector3 position)
        {
            _target = _shootingTarget.GetTarget(position, _weapon.Description.ShootRadius);

            _nextShootTimerSec -= _timer.DeltaTime;
            if ( _nextShootTimerSec < 0 )
            {
                if (HasTarget)
                    _weapon.Shoot(position, TargetPosition);

                _nextShootTimerSec = _weapon.Description.ShootFrequencySec;
            }

            _animationSettings.SetShooting(HasTarget);
            if (HasTarget)
                _animationSettings.ShootingTargetPosition = TargetPosition;
        }

        public void SetWeapon( WeaponModel weapon)
        {
            _weapon = weapon;
        }
    }
}