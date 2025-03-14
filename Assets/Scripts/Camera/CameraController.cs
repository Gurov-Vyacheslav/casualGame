using LearnGame.Spawners;
using System;
using UnityEngine;

namespace LearnGame.Camera
{
    public class CameraController : MonoBehaviour
    {
        [Header("Follow Settings")]
        [SerializeField]
        private Vector3 _followCameraOffset = Vector3.zero;
        [SerializeField]
        private Vector3 _rotationOffset = Vector3.zero;


        [Header("Win Settings")]
        [SerializeField]
        private Vector3 _winCameraOffset = new Vector3(-4f, 3f, 0f);
        [SerializeField]
        private Vector3 _winRotationOffset = new Vector3(0f, 1.5f, 0f); 
        [SerializeField]
        private float _moveSpeed = 3f;

        private Vector3 _currentCameraOffset;
        private Vector3 _currentRotationOffset;

        private BaseCharacterView _character;
        protected void Start()
        {
            _character = CharacterSpawnersController.Instance.Player;
            if (_character == null)
                throw new NullReferenceException($"Follow camera can't folow null player - {nameof(_character)}.");
            _currentCameraOffset = _followCameraOffset;
            _currentRotationOffset = _rotationOffset;
        }
        
        protected void LateUpdate()
        { 
            if (_character == null) return;

            transform.position = Vector3.Lerp(
                transform.position,
                _character.transform.position + _currentCameraOffset,
                _moveSpeed * Time.deltaTime
            );

            Vector3 targetRotation = _currentRotationOffset - _currentCameraOffset;
            Quaternion targetQuaternion = Quaternion.LookRotation(targetRotation);

            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                targetQuaternion,
                _moveSpeed * Time.deltaTime
            );
        }

        public void SetCharacter(BaseCharacterView character)
        {
            _character = character;
        }
        public void ReportPlayerWon()
        {
            _currentCameraOffset = _winCameraOffset;
            _currentRotationOffset = _winRotationOffset;
        }
    }
}

