using LearnGame.Camera;
using LearnGame.Spawners;
using UnityEngine;

namespace LearnGame.Enemy
{
    [RequireComponent(typeof(EnemyDirectionController), typeof(EnemyAiController))]
    public class EnemyCharacterView : BaseCharacterView
    {
        private CameraController _cameraController;

        protected override void Awake()
        {
            base.Awake();
            _cameraController = UnityEngine.Camera.main.GetComponent<CameraController>();
        }

        protected override void OnDestroy()
        {
            CharacterSpawnersController.instance.ReportKillEnemy();
        }

        protected override bool CheckVictory()
        {
            if (CharacterSpawnersController.instance.PlayerWasKilled &&
                CharacterSpawnersController.instance.CountEnemy - CharacterSpawnersController.instance.CurrentCountKilledEnemy == 1)
            {
                _characterAnimatorController.IsWinning();
                _cameraController?.SetCharacter(this);
                _cameraController.ReportPlayerWon();
                return true;
            }
            return false;
        }
    }
}

