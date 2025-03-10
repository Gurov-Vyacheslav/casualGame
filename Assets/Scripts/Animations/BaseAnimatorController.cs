﻿using UnityEngine;

namespace LearnGame.Animations
{
    [RequireComponent(typeof(Animator))]
    public class BaseAnimatorController : MonoBehaviour
    {
        protected Animator _animator;

        protected virtual void Awake()
        {
            _animator = GetComponent<Animator>();
        }
    }
}