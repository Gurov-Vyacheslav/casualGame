using UnityEngine;

namespace LearnGame.Ui
{
    public class LosePanel : MonoBehaviour
    {
        [SerializeField]
        private GameManager _gameManager;

        void Start()
        {
            _gameManager.Loss += ShowPanel;
            gameObject.SetActive(false);
        }

        private void ShowPanel()
        {
            _gameManager.Loss -= ShowPanel;
             gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            if (_gameManager != null)
            {
                _gameManager.Loss -= ShowPanel;
            }
        }
    }
}
