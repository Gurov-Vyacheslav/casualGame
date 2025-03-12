using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LearnGame.Animations
{
    public interface ICharacterMovingAnimationSetting
    {
        public void SetMoving(bool isMoving);
        public void SetRunning(bool isRunning);
    }
}
