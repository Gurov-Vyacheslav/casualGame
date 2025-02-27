using LearnGame.Camera;
using LearnGame.Movement;
using UnityEngine;

namespace LearnGame
{
    [RequireComponent(typeof(PlayerMovementDirectionController))]
    public class PlayerCharacter : BaseCharacter
    {
        private CameraController _cameraController;
        protected override void Awake() 
        {
            base.Awake();
            _cameraController = UnityEngine.Camera.main.GetComponent<CameraController>();
            _cameraController.SetCharacter(this);
        }

        protected override void OnDestroy()
        {
            _characterSpawnerController.ReportKillPlayer();
        }

        protected override bool CheckVictory()
        {
            if (_characterSpawnerController.CountEnemy == _characterSpawnerController.CurrentCountKilledEnemy)
            {
                _characterAnimatorController.IsWinning();
                _cameraController.ReportPlayerWon();
                return true;
            }
            return false;
        }
    }
}

