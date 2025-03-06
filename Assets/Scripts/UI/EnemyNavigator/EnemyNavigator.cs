using LearnGame.Enemy;
using System.Collections.Generic;
using UnityEngine;

namespace LearnGame.UI.EnemyNavigator
{
    public class EnemyNavigator : MonoBehaviour
    {
        [SerializeField]
        private GameManager _manager;
        [SerializeField]
        private EnemyPointer _pointerPrefub;
        [SerializeField]
        private int _maxSizePx = 100;
        [SerializeField]
        private int _minSizePx = 50;
        [SerializeField]
        private float _maxDistanceEffect = 100f;
        [SerializeField]
        private float _minDistanceEffect = 10f;


        private BaseCharacter _player;
        private UnityEngine.Camera _camera;
        private RectTransform _rectTransform;

        private void Awake()
        {
            _manager.SpawnEnemyPointer += SpawnPointer;
            _manager.SpawnPlayer += SetPlayer;
            _camera = UnityEngine.Camera.main;
            _rectTransform = GetComponent<RectTransform>();
            
        }

        private void SpawnPointer(EnemyCharacter enemy)
        {
            var pointer = Instantiate(_pointerPrefub, Vector3.zero, Quaternion.identity, this.transform);
            pointer.Initialize(enemy, _player, _camera, _rectTransform, _minSizePx, _maxSizePx, _maxDistanceEffect, _minDistanceEffect);
        }

        private void OnDestroy()
        {
            _manager.SpawnEnemyPointer -= SpawnPointer;
        }

        private void SetPlayer(BaseCharacter player)
        {
            _player = player;
        }

    }
}
