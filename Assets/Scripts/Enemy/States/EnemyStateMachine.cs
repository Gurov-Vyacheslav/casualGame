using LearnGame.FSM;
using LearnGame.Movement;
using System.Collections.Generic;

namespace LearnGame.Enemy.States
{
    public class EnemyStateMachine: BaseStateMachine
    {
        private const float NavMeshTurnOffDistance = 5f;

        public EnemyStateMachine(EnemyDirectionController enemyDirectionController,
            NavMesher navMesher, EnemyTarget target)
        {
            var idleSate = new IdleState();
            var findWaySate = new FindWayState(target, navMesher, enemyDirectionController);
            var moveForwardState = new MoveForwardState(target, enemyDirectionController);

            SetInitialState(idleSate);
            AddState(state: idleSate, transotions: new List<Transition>
                {
                    new Transition(
                        findWaySate,
                        () => target.DistanceToClosestFromAgent() > NavMeshTurnOffDistance),
                    new Transition(
                        moveForwardState,
                        () => target.DistanceToClosestFromAgent() <= NavMeshTurnOffDistance)
                }
            );
            AddState(state: findWaySate, transotions: new List<Transition>
                {
                    new Transition(
                        idleSate,
                        () => target.Closest == null),
                    new Transition(
                        moveForwardState,
                        () => target.DistanceToClosestFromAgent() <= NavMeshTurnOffDistance)
                }
            );
            AddState(state: moveForwardState, transotions: new List<Transition>
                {
                    new Transition(
                        idleSate,
                        () => target.Closest == null),
                    new Transition(
                        findWaySate,
                        () => target.DistanceToClosestFromAgent() > NavMeshTurnOffDistance)
                }
            );
        }
    }
}
