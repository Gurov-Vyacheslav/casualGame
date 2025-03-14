using UnityEngine;

namespace LearnGame.UI.EnemyNavigator
{
    [CreateAssetMenu(fileName = nameof(PointerFactory), menuName = nameof(PointerFactory))]
    public class PointerFactory: ScriptableObject
    {
        [SerializeField]
        private EnemyPointerView _pointerPrefab;

        [SerializeField]
        private PointerDescriotion _description;

        public EnemyPointerView Create(Transform pointerParent, Transform enemy, Transform player, UnityEngine.Camera camera)
        {
            var pointer = Instantiate(_pointerPrefab, pointerParent);
            var model = new EnemyPointerModel(_description, Screen.width, Screen.height);
            pointer.Initialize(enemy, player, camera, model);

            return pointer;
        }
    }
}

