﻿using LearnGame.FSM;
using UnityEngine;

namespace LearnGame.Enemy.States
{
    public class EscapeState: BaseState
    {
        private readonly EnemyTargetGO _target;
        private readonly EnemyDirectionController _enemyDirectionController;

        private Vector3 _currentPoint;
        public EscapeState(EnemyTargetGO target, EnemyDirectionController enemydirectionController)
        {
            _target = target;
            _enemyDirectionController = enemydirectionController;
        }

        public override void Execute()
        {
            if (_target.Closest == null) return;

            Vector3 targetPosition = _target.Closest.transform.position;
            if (_currentPoint != targetPosition)
            {
                _currentPoint = targetPosition;
                _enemyDirectionController.UpdateMovementDirection(_currentPoint, true);
                _enemyDirectionController.UpdateBoostIncluded(true);
            }
        }
    }
}
