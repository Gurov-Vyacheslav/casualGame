using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LearnGame.Animations
{
    public interface ICharacterShootingAnimationSettings
    {
        public Vector3 ShootingTargetPosition { get; set; }
        public void SetShooting(bool isShooting);
    }
}
