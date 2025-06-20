using LearnGame.Camera;
using LearnGame.Movement;
using LearnGame.Spawners;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LearnGame
{
#if UNITY_ANDROID
    [RequireComponent(typeof(JoystickPlayerMovementDirectionController))]
#elif UNITY_EDITOR || UNITY_STANDALONE
    [RequireComponent(typeof(PlayerMovementDirectionController))]
#endif
    public class PlayerCharacterView : BaseCharacterView
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
            CharacterSpawnersController.Instance.ReportKillPlayer();
        }

        protected override bool CheckVictory()
        {
            if (CharacterSpawnersController.Instance.CountEnemy == CharacterSpawnersController.Instance.CurrentCountKilledEnemy)
            {
                if (!_isWin)
                {
                    _isWin = true;
                    if (SceneManager.GetActiveScene().buildIndex != 0)
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

