using LearnGame.FSM;
using UnityEngine;

namespace LearnGame.Enemy.States
{
    public class FindWayState: BaseState
    {
        private const float MaxDIstanceBetweenRealAndCaluculatePoints = 3f;

        private readonly EnemyTarget _target;
        private readonly NavMesher _navMesher;
        private readonly EnemyDirectionController _enemyDirectionController;

        private Vector3 _currentPoint;

        public FindWayState(EnemyTarget target, NavMesher navMesher, EnemyDirectionController enemydirectionController)
        {
            _target = target;
            _navMesher = navMesher;
            _enemyDirectionController = enemydirectionController;
        }

        public override void Execute()
        {
            Vector3 targetPosition = _target.Closest.transform.position;
            if (!_navMesher.IsPathCalculated || _navMesher.DistanceToTargetPointFrom(targetPosition) > MaxDIstanceBetweenRealAndCaluculatePoints)
                _navMesher.CalculatePath(targetPosition);
            var currentPoint = _navMesher.GetCurrentPoint();
            if (_currentPoint !=  currentPoint)
            {
                _currentPoint = currentPoint;
                _enemyDirectionController.UpdateMovementDirection(_currentPoint);
            }
        }
    }
}
