﻿using LearnGame.Boosters;
using UnityEngine;

namespace LearnGame.Movement
{
    [RequireComponent(typeof(CharacterController), typeof(PowerUpController))]
    public class CharacterMovementController : MonoBehaviour
    {
        private static readonly float SqrEpsilon = Mathf.Epsilon*Mathf.Epsilon;

        [SerializeField]
        private float _speed = 1f;
        [SerializeField]
        private float _boostSpeed = 2f;
        [SerializeField]
        private float _maxRadianDelta = 10f;


        public Vector3 MovementDirection {  get; set; }
        public Vector3 LookDirection { get; set; }

        public bool BoostSpeedIncluded { get; set; }

        private PowerUpController _powerUpController;

        private CharacterController _characterController;

        protected void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _powerUpController = GetComponent<PowerUpController>();
        }
   
        protected void Update()
        {
            Translate();

            if (_maxRadianDelta > 0f && LookDirection != Vector3.zero)
                Rotate();
        }

        private void Translate()
        {
            var delta = MovementDirection * _speed * Time.deltaTime;
            
            if (BoostSpeedIncluded) delta *= _boostSpeed;

            if (_powerUpController.BoostInclude())
                delta *= _powerUpController.Booster.BoostSpeed;

            _characterController.Move(delta);
        }
        
        private void Rotate()
        {
            var currentLookDirection = transform.rotation * Vector3.forward;
            float sqrMagnitude = (currentLookDirection - LookDirection).sqrMagnitude;

            if (sqrMagnitude > SqrEpsilon)
            {
                var newRotation = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.LookRotation(LookDirection, Vector3.up),
                    _maxRadianDelta * Time.deltaTime);
                transform.rotation = newRotation;
            }
        }
    }
}