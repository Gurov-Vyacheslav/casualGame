using LearnGame.Movement;
using LearnGame.Timer;
using UnityEngine;

namespace LearnGame.Shooting
{
    public class BulletMovementController: IMemorizeMovable
    {
        private float _speed;
        private ITimer _timer;
        public BulletMovementController(float speed, ITimer timer) 
        {
            _speed = speed;
            _timer = timer;
        }
        public float CurrentDistance { get; private set; } = 0;
        public Vector3 Translate(Vector3 movementDirection)
        {
            var delta = _speed * _timer.DeltaTime;
            CurrentDistance += delta;
            return movementDirection * delta;
        }
    }
}
