using UnityEngine;
using UnityEngine.UI;

namespace LearnGame.UI.EnemyNavigator
{
    public class EnemyPointerView : MonoBehaviour
    {
        private Transform _enemy;
        private Transform _player;
        private UnityEngine.Camera _mainCamera;
        private RectTransform _pointer;
        private Image _pointImage;

        private EnemyPointerModel _model;


        public void Initialize(Transform enemy, Transform player, UnityEngine.Camera mainCamera, EnemyPointerModel model)
        {
            _enemy = enemy;
            _player = player;
            _mainCamera = mainCamera;
            _model = model;

            _pointer = GetComponent<RectTransform>();
            _pointImage = GetComponent<Image>();
        }

        private void LateUpdate()
        {
            if (_enemy == null || _player == null)
            {
                _pointImage.enabled = false;
                return;
            }
            _pointImage.enabled = !_model.IsEnemyVisible();
            
            _model.UpdateCharacterPositions(_player.position, _enemy.position);

            _pointer.sizeDelta = _model.Size();

            Vector3 screenPosEnemy = _mainCamera.WorldToScreenPoint(_enemy.position);
            _pointer.anchoredPosition = _model.Position(screenPosEnemy);
            _pointer.localRotation = _model.Rotation(screenPosEnemy);
        }
    }
}
