using LearnGame.FSM;
using System.Collections.Generic;
using UnityEngine;

namespace LearnGame.Enemy.States
{
    public class EnemyStateMachine: BaseStateMachine
    {
        private const float NavMeshTurnOffDistance = 3f;

        public EnemyStateMachine(EnemyCharacter enemyCharacter, EnemyDirectionController enemyDirectionController,
            NavMesher navMesher, EnemyTarget target, float minHpForEscapePercent, float probabilityEscapePercent)
        {
            var idleSate = new IdleState(enemyDirectionController);
            var findWaySate = new FindWayState(target, navMesher, enemyDirectionController);
            var moveForwardState = new MoveForwardState(target, enemyDirectionController);
            var escapeState = new EscapeState(target, enemyDirectionController);

            
            bool NeedEscape()
            {
                float currentHealth = enemyCharacter.Health;
                float maxHealth = enemyCharacter.MaxHealth;
                float healthPercentage = MathUtils.ToPercentage(currentHealth, maxHealth);
                GameObject closestTarget = target.Closest;

                bool isLowHealth = healthPercentage <= minHpForEscapePercent;
                bool closestIsCharacter = closestTarget != null && LayerUtils.IsCharacter(closestTarget);
                bool isChanceTriggered = Random.value * 100f < probabilityEscapePercent;

                return isLowHealth && closestIsCharacter && isChanceTriggered;
            }

            SetInitialState(idleSate);
            AddState(state: idleSate, transitions: new List<Transition>
                {
                    new Transition(
                        escapeState,
                        () => NeedEscape()),
                    new Transition(
                        findWaySate,
                        () => target.DistanceToClosestFromAgent() > NavMeshTurnOffDistance),
                    new Transition(
                        moveForwardState,
                        () => target.DistanceToClosestFromAgent() <= NavMeshTurnOffDistance)
                }
            );
            AddState(state: findWaySate, transitions: new List<Transition>
                {
                    new Transition(
                        idleSate,
                        () => target.Closest == null),
                    new Transition(
                        moveForwardState,
                        () => target.DistanceToClosestFromAgent() <= NavMeshTurnOffDistance)
                }
            );
            AddState(state: moveForwardState, transitions: new List<Transition>
                {
                    new Transition(
                        escapeState,
                        () => NeedEscape()),
                    new Transition(
                        idleSate,
                        () => target.Closest == null),
                    new Transition(
                        findWaySate,
                        () => target.DistanceToClosestFromAgent() > NavMeshTurnOffDistance)
                }
            );
            AddState(state: escapeState, transitions: new List<Transition>
                {
                    new Transition(
                        idleSate,
                        () => target.Closest == null || target.MoveToSafeDistance())
                }
            );

        }
    }
}
