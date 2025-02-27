using UnityEngine;

namespace LearnGame.Animations
{
    public class CharacterAnimatorController : BaseAnimatorController
    {
        public void SetMoving(bool isMoving) => _animator.SetBool("IsMoving", isMoving);

        public void SetRunning(bool isRunning) => _animator.SetBool("IsRunning", isRunning && _animator.GetBool("IsMoving"));

        public void SetShooting(bool isShooting) => _animator.SetBool("IsShooting", isShooting && !_animator.GetBool("IsDead"));

        public void IsWinning() => _animator.SetTrigger("IsWin");

        public void IsDead() => _animator.SetBool("IsDead", true);

        public Vector3 TargetPosition { get; set; }
        public bool HasTarget {  get; set; }

        [SerializeField] private float _highLook = 1.3f;



        private void OnAnimatorIK(int layerIndex)
        {
            if (!HasTarget)
            {
                return;
            }

            _animator.SetLookAtWeight(1, 0.5f, 1);
            TargetPosition = new Vector3(TargetPosition.x, _highLook, TargetPosition.z);
            Vector3 directionToTarget = TargetPosition - transform.position;

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
