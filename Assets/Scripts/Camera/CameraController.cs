using LearnGame.Animations;
using System;
using UnityEngine;

namespace LearnGame.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _followCameraOffset = Vector3.zero;

        [SerializeField]
        private Vector3 _rotationOffset = Vector3.zero;

        [SerializeField]
        private BaseCharacter _character;

        protected void Start()
        {
            if (_character == null)
                throw new NullReferenceException($"Follow camera can't folow null player - {nameof(_character)}.");
        }
        
        protected void LateUpdate()
        {
            if (_character == null) return;
            Vector3 targetRotation = _rotationOffset - _followCameraOffset;

            transform.position = _character.transform.position + _followCameraOffset;
            transform.rotation = Quaternion.LookRotation(targetRotation, Vector3.up);
        }

        public void SetCharacter(BaseCharacter character)
        {
            _character = character;
        }
        public void ReportPlayerWon()
        {
            var cameraAmimatorController = GetComponentInParent<CameraAmimatorController>();
            cameraAmimatorController.Scale();
        }
    }
}

