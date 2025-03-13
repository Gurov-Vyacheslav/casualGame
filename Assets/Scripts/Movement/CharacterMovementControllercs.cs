using LearnGame.Boosters;
using LearnGame.Timer;
using UnityEngine;

namespace LearnGame.Movement
{
    public class CharacterMovementController: IMovementController
    {
        private static readonly float SqrEpsilon = Mathf.Epsilon*Mathf.Epsilon;

        private readonly ITimer _timer;

        private readonly float _speed;
        private readonly float _boostSpeed;
        private readonly float _maxRadianDelta;

        public bool BoostSpeedIncluded { get; set; }

        private PowerUpController _powerUpController;


        public CharacterMovementController(ICharacterConfig config, ITimer timer, PowerUpController powerUpController)
        {
            _speed = config.Speed;
            _boostSpeed = config.BoostSpeed;
            _maxRadianDelta = config.MaxRadiansDelta;
            _timer = timer;
            _powerUpController = powerUpController;
        }
   
        public Vector3 Translate(Vector3 movementDirection)
        {
            var currentSpeed = _speed;

            if (BoostSpeedIncluded) currentSpeed *= _boostSpeed;

            if (_powerUpController.BoostInclude)
                currentSpeed *= _powerUpController.Booster.BoostSpeed;

            var delta = movementDirection * currentSpeed * _timer.DeltaTime;

            return delta;
        }

        public Quaternion Rotate(Quaternion currentRotation, Vector3 lookDirection)
        {
            if (_maxRadianDelta > 0f && lookDirection != Vector3.zero)
            { 
                    var currentLookDirection = currentRotation * Vector3.forward;
                float sqrMagnitude = (currentLookDirection - lookDirection).sqrMagnitude;

                if (sqrMagnitude > SqrEpsilon)
                {
                    var newRotation = Quaternion.Slerp(
                        currentRotation,
                        Quaternion.LookRotation(lookDirection, Vector3.up),
                        _maxRadianDelta * _timer.DeltaTime);
                    return newRotation;
                }
            }
            return currentRotation;
        }
        
    }
}