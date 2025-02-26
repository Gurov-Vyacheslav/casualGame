﻿using LearnGame.FSM;
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



            SetInitialState(idleSate);
            AddState(state: idleSate, transotions: new List<Transition>
                {
                    new Transition(
                        escapeState,
                        () => enemyCharacter.Health/enemyCharacter.MaxHealth*100 <= minHpForEscapePercent && Random.Range( 0.0f, 100.0f ) <= probabilityEscapePercent),
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
                        escapeState,
                        () => enemyCharacter.Health/enemyCharacter.MaxHealth*100 <= minHpForEscapePercent && Random.Range( 0.0f, 100.0f ) <= probabilityEscapePercent),
                    new Transition(
                        idleSate,
                        () => target.Closest == null),
                    new Transition(
                        findWaySate,
                        () => target.DistanceToClosestFromAgent() > NavMeshTurnOffDistance)
                }
            );
            AddState(state: escapeState, transotions: new List<Transition>
                {
                    new Transition(
                        idleSate,
                        () => target.Closest == null || target.MoveToSafeDistance())
                }
            );

        }
    }
}
