using LearnGame.Exceptions;
using LearnGame.FSM;
using System;
using UnityEngine;

namespace LearnGame.Enemy.States
{
    public class EscapeState: BaseState
    {
        private readonly EnemyTarget _target;
        private readonly EnemyDirectionController _enemyDirectionController;

        private Vector3 _currentPoint;
        public EscapeState(EnemyTarget target, EnemyDirectionController enemydirectionController)
        {
            _target = target;
            _enemyDirectionController = enemydirectionController;
        }

        public override void Execute()
        {
           /* Debug.Log("Убегает");*/
            try
            {
                Vector3 targetPosition = _target.Closest.transform.position;
                if (_currentPoint != targetPosition)
                {
                    _currentPoint = targetPosition;
                    _enemyDirectionController.UpdateMovementDirection(_currentPoint, true);
                    _enemyDirectionController.UpdateBoostIncluded(true);
                }
            }
            catch (Exception NotStoped)
            {
                throw new DoNotKnowWhatDoingException(DoNotKnowWhatDoingException.BaseMessage, NotStoped);
            }
        }
    }
}
