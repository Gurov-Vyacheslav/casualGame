using LearnGame.FSM;
using UnityEngine;

namespace LearnGame.Enemy.States
{
    public class IdleState: BaseState
    {
        private readonly EnemyDirectionController _enemyDirectionController;
        public IdleState(EnemyDirectionController enemyDirectionController) 
        {
            _enemyDirectionController = enemyDirectionController;
        }
        public override void Execute()
        {
            _enemyDirectionController.UpdateBoostIncluded(false);
        }
    }
}
