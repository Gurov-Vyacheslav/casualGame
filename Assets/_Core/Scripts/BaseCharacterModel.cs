using LearnGame.Animations;
using LearnGame.Movement;
using LearnGame.Shooting;
using UnityEngine;

namespace LearnGame
{
    public class BaseCharacterModel
    {
        public readonly float MaxHealth;
        public TransformModel Transform { get; private set; }

        public float Health { get; private set; }
        public bool IsDead { get; private set; } = false;

        public bool SetBaseWeapon { get; private set; } = false;

        public bool MovingForward { get; private set; }
        public Vector3 ShootingTargetPosition => _shootingController.TargetPosition;

        private readonly IMovementController _characterMovementController;
        private readonly ShootingController _shootingController;
        private readonly ICharacterMovingAnimationSetting _movingAnimationSetting;

        public BaseCharacterModel(IMovementController movementController, ShootingController shootingController,
            ICharacterConfig characterConfig, ICharacterMovingAnimationSetting movingAnimationSetting)
        {
            _characterMovementController = movementController;
            _shootingController = shootingController;

            Health = characterConfig.Health;
            MaxHealth = Health;

            _movingAnimationSetting = movingAnimationSetting;
        }

        public void Initialize(Vector3 position, Quaternion rotation)
        {
            Transform = new TransformModel(position, rotation);
        }

        public void Move(Vector3 direction, bool boostSpeedIncluded)
        {
            var lookDirection = direction;

            if (_shootingController.HasTarget)
                lookDirection = (_shootingController.TargetPosition - Transform.Position).normalized;

            CheckAnimationMovement(lookDirection, direction);

            _characterMovementController.BoostSpeedIncluded = boostSpeedIncluded;

            Transform.Position += _characterMovementController.Translate(direction);
            Transform.Rotation = _characterMovementController.Rotate(Transform.Rotation, lookDirection);

            _movingAnimationSetting.SetMoving(direction != Vector3.zero);
            _movingAnimationSetting.SetRunning(boostSpeedIncluded);
        }
        private void CheckAnimationMovement(Vector3 lookDirection, Vector3 movementDirection)
        {
            var angle = Vector3.Angle(lookDirection, movementDirection);
            MovingForward = angle < 90;
        }

        public void Damage(float damage)
        {
            Health -= damage;
            IsDead = Health <= 0;
        }

        public void TryShoot(Vector3 shotPosition)
        {
            _shootingController.TryShoot(shotPosition);
        }


        public void SetWeapon(WeaponModel weapon, bool isBaseWeapon)
        {
            _shootingController.SetWeapon(weapon);
            SetBaseWeapon = !isBaseWeapon;
        }
    }
}

