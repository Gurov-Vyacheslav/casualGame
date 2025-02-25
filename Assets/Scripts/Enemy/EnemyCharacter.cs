using LearnGame.Camera;
using UnityEngine;

namespace LearnGame.Enemy
{
    [RequireComponent(typeof(EnemyDirectionController), typeof(EnemyAiController))]
    public class EnemyCharacter : BaseCharacter
    {
        public float MaxHealth { get; private set; }

        private CameraController _cameraController;

        protected override void Awake()
        {
            base.Awake();
            MaxHealth = Health;
            _cameraController = UnityEngine.Camera.main.GetComponent<CameraController>();
        }

        protected override void OnDestroy()
        {
            _characterSpawnerController.ReportKillEnemy();
        }

        protected override bool CheckVictory()
        {
            if (_characterSpawnerController.PlayerWasKilled &&
                _characterSpawnerController.CountEnemy - _characterSpawnerController.CurrentCountKilledEnemy == 1)
            {
                _characterAnimatorController.IsWinning();
                _cameraController.SetPlayer(this);
                _cameraController.ReportPlayerWon();
                return true;
            }
            return false;
        }
    }
}

