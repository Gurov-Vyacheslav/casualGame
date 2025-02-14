using LearnGame.Movement;
using UnityEngine;

namespace LearnGame.Enemy
{
    public class EnemyDirectionController : MonoBehaviour, IMovementDirectionSourse
    {

        public Vector3 MovementDirection { get; private set; }
        public bool BoostIncluded { get; private set; } = false;

        public void UpdateMovementDirection(Vector3 targetPosition, bool escape = false)
        {
            var realDirection = targetPosition - transform.position;
            if (escape) realDirection = -realDirection;
            MovementDirection = new Vector3(realDirection.x, 0, realDirection.z).normalized;
        }
        public void UpdateBoostIncluded(bool boostIncluded) => BoostIncluded = boostIncluded;
    }
}