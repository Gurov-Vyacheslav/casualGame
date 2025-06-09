using UnityEngine;

namespace LearnGame.Animations
{
    public class CharacterAnimatorController : BaseAnimatorController, 
        ICharacterShootingAnimationSettings, ICharacterMovingAnimationSetting, ICharacterPowerUpAnimationSetting
    {

        [SerializeField] private const float  _highLook = 1.3f;
        [SerializeField] private const float _baseSpeed = 1f;

        public Vector3 ShootingTargetPosition { get; set; }
        private bool _hasTarget;

        public void SetMoving(bool isMoving) => _animator.SetBool("IsMoving", isMoving);
        public void MovingBackwards() => _animator.SetFloat("Speed", -Mathf.Abs(_animator.GetFloat("Speed")));
        public void MovingForward() => _animator.SetFloat("Speed", Mathf.Abs(_animator.GetFloat("Speed")));

        public void SetRunning(bool isRunning) => _animator.SetBool("IsRunning", isRunning && _animator.GetBool("IsMoving"));

        public void SetShooting(bool isShooting)
        {
            _hasTarget = isShooting && !_animator.GetBool("IsDead");
            _animator.SetBool("IsShooting", _hasTarget);
        }
        public void SetBoostSpeed(float n = 1f)
        {
            _animator.SetFloat("Speed", _baseSpeed * n);
        }

        public void IsWinning()
        {
            _animator.SetTrigger("IsWin");
            SetShooting(false);
        }

        public void IsDead()
        {
            _animator.SetBool("IsDead", true);
            SetShooting(false);
        }


        protected override void Awake()
        {
            base.Awake();
            _animator.SetFloat("Speed", _baseSpeed);
        }

        private void OnAnimatorIK(int layerIndex)
        {
            if (!_hasTarget) return;

            _animator.SetLookAtWeight(1, 0.5f, 1);
            ShootingTargetPosition = new Vector3(ShootingTargetPosition.x, _highLook, ShootingTargetPosition.z);
            Vector3 directionToTarget = ShootingTargetPosition - transform.position;

            Quaternion rotation = Quaternion.Euler(0, 30, 0);
            Vector3 rotatedDirection = rotation * directionToTarget;
            Vector3 lookAtPosition = transform.position + rotatedDirection;

            _animator.SetLookAtPosition(lookAtPosition);
        }

        public void OnDeathAnimationEnd()
        {
            Destroy(gameObject);
        }

    }
}
