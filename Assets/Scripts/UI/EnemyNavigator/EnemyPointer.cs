using LearnGame.Enemy;
using UnityEngine;
using UnityEngine.UI;

namespace LearnGame.UI.EnemyNavigator
{
    public class EnemyPointer : MonoBehaviour
    {
        private EnemyCharacter _enemy;
        private BaseCharacter _player;
        private UnityEngine.Camera _mainCamera;
        private RectTransform _enemyNavigator;
        private RectTransform _pointer;
        private Image _pointImage;

        private int _maxSizePx;
        private int _minSizePx;
        private float _maxDistanseEffect;
        private float _minDistanseEffect;

        [SerializeField]
        public float _edgeOffset = 50f;


        public void Initialize(EnemyCharacter enemy, BaseCharacter player, UnityEngine.Camera mainCamera, RectTransform enemyNavigator,
            int minSizePx, int maxSizePx, float maxDistanseEffect, float minDistanceEffect, Color color)
        {
            _enemy = enemy;
            _player = player;
            _mainCamera = mainCamera;
            _enemyNavigator = enemyNavigator;
            _minSizePx = minSizePx;
            _maxSizePx = maxSizePx;
            _maxDistanseEffect = maxDistanseEffect;
            _minDistanseEffect= minDistanceEffect;

            _pointer = GetComponent<RectTransform>();
            _pointImage = GetComponent<Image>();

            GetComponent<Image>().color = color;
        }

        private void LateUpdate()
        {
            if (_enemy == null || _player == null)
            {
                Debug.Log("nooo");
                _pointImage.enabled = false;
                return;
            }
            Vector2 screenPosEnemy = _mainCamera.WorldToScreenPoint(_enemy.transform.position);

            _pointImage.enabled = !IsEnemyVisible(screenPosEnemy);
 
            UpdateSize();
            UpdateTransform(screenPosEnemy);
        }

        private bool IsEnemyVisible(Vector3 screenPosEnemy)
        {
            /*if (screenPosEnemy.z < 0) return false;*/

            float currentDistance = Vector3.Distance(_player.transform.position, _enemy.transform.position);
            bool isVisible = currentDistance < _minDistanseEffect;
            Debug.Log(isVisible);
            return isVisible;
        }

        private void UpdateSize()
        {
            float currentDistance = Vector3.Distance(_player.transform.position, _enemy.transform.position);
            if (currentDistance > _maxDistanseEffect)
                currentDistance = _maxDistanseEffect;

            float size = GetSizeProrateDistance(currentDistance);
            float inversionSize = _maxSizePx - size;
            Vector2 newSize = new Vector2(inversionSize, inversionSize);
            _pointer.sizeDelta = newSize;
        }

        private float GetSizeProrateDistance(float disytance)
        {
            return _minSizePx + disytance * (_maxSizePx - _minSizePx) / _maxDistanseEffect;
        }

        private void UpdateTransform(Vector3 screenPosEnemy)
        {
            Vector3 screenPosCenter = new Vector3(Screen.width, Screen.height, 0)/2;

            Vector3 direction = (screenPosEnemy - screenPosCenter).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Vector3 screenBounds = new Vector3(Screen.width, Screen.height, 0) - new Vector3(_edgeOffset, _edgeOffset, 0);

            float intersection = Mathf.Min(
                Mathf.Abs((screenBounds.x - screenPosCenter.x) / direction.x),
                Mathf.Abs((screenBounds.y - screenPosCenter.y) / direction.y)
            );


            Vector3 edgePosition = screenPosCenter + direction * intersection;

            _pointer.anchoredPosition = edgePosition;
            _pointer.localRotation = Quaternion.Euler(0, 0, angle + 90);
        }
    }
}
