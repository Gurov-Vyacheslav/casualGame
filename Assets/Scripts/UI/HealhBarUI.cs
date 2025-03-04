using UnityEngine;
using UnityEngine.UI;
using LearnGame.Enemy;

namespace LearnGame.UI
{
    public class HealhBarUI : MonoBehaviour
    {

        [SerializeField]
        private Image _healthFill;

        private BaseCharacter _enemyCharacter;
        private Canvas _canvas;

        private UnityEngine.Camera _camera;

        private void Start()
        {
            _enemyCharacter = GetComponentInParent<BaseCharacter>();
            _canvas = GetComponent<Canvas>();
            _camera = UnityEngine.Camera.main;

            _canvas.renderMode = RenderMode.WorldSpace;
            _canvas.worldCamera = _camera;
        }
        private void LateUpdate()
        {
            _healthFill.fillAmount = _enemyCharacter.Health / _enemyCharacter.MaxHealth;

            transform.rotation = _camera.transform.rotation;
        }

    }
}