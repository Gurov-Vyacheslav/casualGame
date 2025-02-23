using UnityEditor;
using UnityEngine;

namespace LearnGame.Spawners
{
    public abstract class BaseSpawner : MonoBehaviour
    {
        [SerializeField]
        protected float _range = 2f;

        [SerializeField]
        protected float _minSpawnIntervalSeconds = 1f;
        [SerializeField]
        protected float _maxSpawnIntervalSeconds = 1f;
        [SerializeField]
        protected Color _colorGizmos = Color.green;

        protected float _currentSpawnIntervalSeconds;
        protected float _currentSpawnTimerSeconds;

        protected virtual void Awake()
        {
            SetNewSpawnIntervalSeconds();
        }

        protected void SetNewSpawnIntervalSeconds()
        {
            _currentSpawnIntervalSeconds = Random.Range(_minSpawnIntervalSeconds, _maxSpawnIntervalSeconds);
        }

        protected void OnDrawGizmos()
        {
            var cashedColor = Handles.color;
            Handles.color = _colorGizmos;
            Handles.DrawWireDisc(transform.position, Vector3.up, _range);
            Handles.color = cashedColor;
        }
    }
}