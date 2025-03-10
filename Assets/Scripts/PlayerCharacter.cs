using LearnGame.Camera;
using LearnGame.Movement;
using UnityEngine;

namespace LearnGame
{
    [RequireComponent(typeof(PlayerMovementDirectionController))]
    public class PlayerCharacter : BaseCharacter
    {
        [SerializeField] private AudioSource _winSound;
        [SerializeField] private AudioSource _loseSound;

        private CameraController _cameraController;

        private bool _isWin = false;
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
                if (!_isWin)
                {
                    _isWin = true;
                    _winSound.Play();
                }

                _characterAnimatorController.IsWinning();
                _cameraController.ReportPlayerWon();
                return true;
            }
            return false;
        }

        protected override void SetSettingBeforeDie()
        {
            base.SetSettingBeforeDie();
            _loseSound.Play();
        }
    }
}

