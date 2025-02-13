using LearnGame.FSM;

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
            _enemydirectionController.UpdateBoostIncluded(false);
        }
    }
}
