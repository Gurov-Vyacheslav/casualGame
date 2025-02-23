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
        private PlayerCharacter _player;
        protected void Start()
        {
            if (_player == null)
                throw new NullReferenceException($"Follow camera can't folow null player - {nameof(_player)}.");
        }
        
        protected void LateUpdate()
        {
            if (_player == null) return;
            Vector3 targetRotation = _rotationOffset - _followCameraOffset;

            transform.position = _player.transform.position + _followCameraOffset;
            transform.rotation = Quaternion.LookRotation(targetRotation, Vector3.up);
        }

        public void SetPlayer(PlayerCharacter player)
        {
            _player = player;
        } 
    }
}

