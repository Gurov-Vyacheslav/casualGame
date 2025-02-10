using LearnGame.PickUp;
using UnityEngine;

namespace LearnGame.Movement
{
    [RequireComponent(typeof(CharacterController))]
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

        private bool _pickedUpBoostSpeed;
        private float _pickUpBoostSpeed;
        private float _boostSpeedIntrvalSeconds;
        private float _currentBoostSpeedTimerSeconds;

        private CharacterController _characterController;


        protected void Awake()
        {
            _characterController = GetComponent<CharacterController>();
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

            if (_pickedUpBoostSpeed)
            {
                if (_currentBoostSpeedTimerSeconds <= _boostSpeedIntrvalSeconds)
                {
                    _currentBoostSpeedTimerSeconds += Time.deltaTime;
                    delta *= _pickUpBoostSpeed;
                }
                else
                {
                    _pickedUpBoostSpeed = false;
                    _currentBoostSpeedTimerSeconds = 0f;
                }
            }
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

        public void GetSpeedBooster(PickUpSpeedBooster pickUpSpeedBooster)
        {
            _pickedUpBoostSpeed = true;
            _pickUpBoostSpeed = pickUpSpeedBooster.BoostSpeed;
            _boostSpeedIntrvalSeconds = pickUpSpeedBooster.BoostSpeedIntrvalSeconds;
        }
    }
}