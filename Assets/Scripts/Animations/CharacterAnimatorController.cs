using LearnGame.Animations;
using UnityEngine;

namespace LearnGame.Animations
{
    public class CharacterAnimatorController : BaseAnimatorController
    {
        public void SetMoving(bool isMoving) => _animator.SetBool("IsMoving", isMoving);

        public void SetRunning(bool isRunning) => _animator.SetBool("IsRunning", isRunning && _animator.GetBool("IsMoving"));

        public void SetShooting(bool isShooting) => _animator.SetBool("IsShooting", isShooting);

        public void IsWinning() => _animator.SetTrigger("IsWin");
    }
}
