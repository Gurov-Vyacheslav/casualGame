using UnityEngine;
using LearnGame.Movement;
using UnityEngine.UIElements;
using System;

namespace LearnGame.Shooting
{
    public class BulletModel
    {
        public event Action Dead;
        public float Damage {  get; private set; }

        public TransformModel Transform { get; private set; }

        private Vector3 _direction;
        private readonly float _maxFlyDistance;
        private IMemorizeMovable _bulletMovementController;


        public BulletModel(Vector3 direction, BulletDescription bulletDescription, IMemorizeMovable bulletMovementController)
        {
            _direction = direction;
            _maxFlyDistance = bulletDescription.BulletMaxFlyDistance;
            Damage = bulletDescription.Damage;
            _bulletMovementController = bulletMovementController;
        }

        public void Initialize(Vector3 position, Quaternion rotation)
        {
            Transform = new TransformModel(position, rotation);
        }

 
        public void Move()
        {
            Transform.Position += _bulletMovementController.Translate(_direction);
        }
        public void Fly()
        {
            if (_bulletMovementController.CurrentDistance >= _maxFlyDistance)
                Dead?.Invoke();
        }
    }
}