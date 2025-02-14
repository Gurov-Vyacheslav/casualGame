using LearnGame.Spowners;
using UnityEngine;

namespace LearnGame.PickUp
{
    public class PickUpSpawner : BaseSpowner
    {
        [SerializeField]
        private int _maxCount = 2;
        [SerializeField]
        private PickUpItem _pickUpPrefab;

        private int _currentCount;

        protected void Update()
        {
            if (_currentCount < _maxCount) 
            {
                _currentSpawnTimerSeconds += Time.deltaTime;
                if (_currentSpawnTimerSeconds > _currentSpawnIntervalSeconds)
                {
                    _currentSpawnTimerSeconds = 0f;
                    SetNewSpawnIntervalSeconds();
                    _currentCount++;

                    var randomPointInsideRange = Random.insideUnitCircle * _range;
                    var randomPosition = new Vector3(randomPointInsideRange.x, 0f, randomPointInsideRange.y) + transform.position;

                    var pickUp = Instantiate(_pickUpPrefab, randomPosition, Quaternion.identity, transform);
                    pickUp.OnPickedUp += OnItemPickedUp;
                }
            }
        }

        private void OnItemPickedUp(PickUpItem pickedUpItem)
        {
            _currentCount--;
            pickedUpItem.OnPickedUp -= OnItemPickedUp; 
        }
    }
}