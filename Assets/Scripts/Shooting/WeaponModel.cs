﻿using System;
using UnityEngine;

namespace LearnGame.Shooting
{
    public class WeaponModel
    {
        public event Action<Vector3, WeaponDescription> Shot;

        public WeaponDescription Description { get; }

        public WeaponModel(WeaponDescription description)
        {
            Description = description;
        }

        public void Shoot(Vector3 shootPosition, Vector3 targetPoint)
        {
            var target = targetPoint - shootPosition;
            target.y = 0;
            target.Normalize();

            Shot?.Invoke(target, Description);
        }
    }
}