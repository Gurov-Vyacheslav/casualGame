using LearnGame.Enemy;
using UnityEngine;

namespace LearnGame.UI.EnemyNavigator
{
    public class EnemyNavigator : MonoBehaviour
    {
        [SerializeField]
        private PointerFactory _pointerFactory;


        private BaseCharacterView _player;
        private UnityEngine.Camera _camera;
        private RectTransform _rectTransform;

        private void Awake()
        {
            GameManager.Instance.SpawnEnemyPointer += SpawnPointer;
            GameManager.Instance.SpawnPlayer += SetPlayer;
            _camera = UnityEngine.Camera.main;
            _rectTransform = GetComponent<RectTransform>();
            
        }

        private void SpawnPointer(EnemyCharacterView enemy)
        {
            _pointerFactory.Create(transform, enemy.transform, _player.transform, _camera);
        }

        private void OnDestroy()
        {
            GameManager.Instance.SpawnEnemyPointer -= SpawnPointer;
        }

        private void SetPlayer(BaseCharacterView player)
        {
            _player = player;
        }

    }
}
