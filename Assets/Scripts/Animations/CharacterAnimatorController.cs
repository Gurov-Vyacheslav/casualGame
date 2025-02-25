namespace LearnGame.Animations
{
    public class CharacterAnimatorController : BaseAnimatorController
    {
        public void SetMoving(bool isMoving) => _animator.SetBool("IsMoving", isMoving);

        public void SetRunning(bool isRunning) => _animator.SetBool("IsRunning", isRunning && _animator.GetBool("IsMoving"));

        public void SetShooting(bool isShooting) => _animator.SetBool("IsShooting", isShooting && !_animator.GetBool("IsDead"));

        public void IsWinning() => _animator.SetTrigger("IsWin");

        public void IsDead() => _animator.SetBool("IsDead", true);

        /*     public Vector3 LookDirection { get; set; }*/

        /*private void Update()
        {
            if (_animator.GetBool("IsShooting"))
            {
                Transform spineBone = _animator.GetBoneTransform(HumanBodyBones.Spine);
                Quaternion targetRotation = Quaternion.LookRotation(LookDirection) * Quaternion.Euler(0, 0, 70);
                spineBone.rotation = Quaternion.Slerp(spineBone.rotation, targetRotation, Time.deltaTime * 5f);
                Debug.Log("Spine bone found: " + spineBone.name);
            }
        }*/
        public void OnDeathAnimationEnd()
        {
            Destroy(gameObject);
        }
    }
}
