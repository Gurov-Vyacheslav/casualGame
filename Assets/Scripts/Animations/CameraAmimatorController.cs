﻿namespace LearnGame.Animations
{
    public class CameraAmimatorController : BaseAnimatorController
    {
        public void Scale() => _animator.SetTrigger("Scale");
    }
}