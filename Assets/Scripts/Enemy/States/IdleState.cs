using LearnGame.FSM;
using UnityEngine;

namespace LearnGame.Enemy.States
{
    public class IdleState: BaseState
    {
        private readonly EnemyDirectionController _enemydirectionController;
        public IdleState(EnemyDirectionController enemyDirectionController) 
        {
            _enemydirectionController = enemyDirectionController;
        }
        public override void Execute()
        {
           /* Debug.Log("Ожидает");*/
            _enemydirectionController.UpdateBoostIncluded(false);
        }
    }
}
