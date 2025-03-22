using UnityEngine;

namespace LearnGame.Enemy
{
    public class EnemyAiControllerView : MonoBehaviour
    {
        private EnemyAiControllerModel _model;

        public void Initialize(EnemyAiControllerModel model)
        {
            _model = model;
        }

        protected void Update()
        {
            _model.FindClosestTarget();
            _model.UpdateStateMashine();
        }
    }
}