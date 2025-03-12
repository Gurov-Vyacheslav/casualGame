using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LearnGame.Animations
{
    public interface ICharacterPowerUpAnimationSetting
    {
        public void SetBoostSpeed(float n = 1f);
    }
}
