using UnityEngine;
using UnityEngine.UI;

namespace LearnGame.UI
{
    public class HealhBarUI : MonoBehaviour
    {

        [SerializeField]
        private Image _healthFill;

        private BaseCharacterView _enemyCharacter;
        private Canvas _canvas;

        private UnityEngine.Camera _camera;

        private void Start()
        {
            _enemyCharacter = GetComponentInParent<BaseCharacterView>();
            _canvas = GetComponent<Canvas>();
            _camera = UnityEngine.Camera.main;

            _canvas.renderMode = RenderMode.WorldSpace;
            _canvas.worldCamera = _camera;
        }
        private void LateUpdate()
        {
            var currentHealth = _enemyCharacter.Model.Health;
            if (currentHealth < 0)
                currentHealth = 0;
            _healthFill.fillAmount = currentHealth / _enemyCharacter.Model.MaxHealth;

            transform.rotation = _camera.transform.rotation;
        }

    }
}