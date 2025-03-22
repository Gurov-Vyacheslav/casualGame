using UnityEngine;

namespace LearnGame.UI.EnemyNavigator
{
    public class EnemyPointerModel
    {
        private PointerDescriotion _pointerDescriotion;
        private Vector3 _playerPosition;
        private Vector3 _enemyPosition;
        private int _screenWidth;
        private int _screenHeight;

        public EnemyPointerModel(PointerDescriotion pointerDescriotion, int screenWidth, int screenHeight)
        {
            _pointerDescriotion = pointerDescriotion;
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
        }

         public void UpdateCharacterPositions(Vector3 playerPosition, Vector3 enemyPosition)
        {
            _enemyPosition = enemyPosition;
            _playerPosition = playerPosition;
        }

        public bool IsEnemyVisible()
        {
            float currentDistance = Vector3.Distance(_playerPosition, _enemyPosition);
            bool isVisible = currentDistance < _pointerDescriotion.MinDistanceEffect;
            return isVisible;
        }

        public Vector2 Size()
        {
            float currentDistance = Vector3.Distance(_playerPosition, _enemyPosition);
            if (currentDistance > _pointerDescriotion.MaxDistanceEffect)
                currentDistance = _pointerDescriotion.MaxDistanceEffect;

            float size = GetSizeProrateDistance(currentDistance);
            float inversionSize = _pointerDescriotion.MaxSizePx - size;
            Vector2 newSize = new Vector2(inversionSize, inversionSize);
            return newSize;
        }

        private float GetSizeProrateDistance(float disytance)
        {
            return _pointerDescriotion.MinSizePx + disytance * 
                (_pointerDescriotion.MaxSizePx - _pointerDescriotion.MinSizePx) / _pointerDescriotion.MaxDistanceEffect;
        }

        public Vector3 Position(Vector3 screenPosEnemy)
        {
            if (screenPosEnemy.z < 0)
            {
                screenPosEnemy *= -1;
            }

            Vector3 screenPosCenter = new Vector2(_screenWidth, _screenHeight) / 2;
            Vector3 direction = (screenPosEnemy - screenPosCenter).normalized;
            Vector3 screenBounds = new Vector3(_screenWidth, _screenHeight, 0) - new Vector3(_pointerDescriotion.EdgeOffset, _pointerDescriotion.EdgeOffset, 0);

            float intersection = Mathf.Min(
                Mathf.Abs((screenBounds.x - screenPosCenter.x) / direction.x),
                Mathf.Abs((screenBounds.y - screenPosCenter.y) / direction.y)
            );
            Vector3 edgePosition = screenPosCenter + direction * intersection;
            return edgePosition;
        }

        public Quaternion Rotation(Vector2 screenPosEnemy)
        {
            Vector2 screenPosCenter = new Vector2(_screenWidth, _screenHeight) / 2;

            Vector2 direction = (screenPosEnemy - screenPosCenter).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            return Quaternion.Euler(0, 0, angle + 90);
        } 
    }
}
