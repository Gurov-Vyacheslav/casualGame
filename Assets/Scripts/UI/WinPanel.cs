using UnityEngine;

namespace LearnGame.Ui
{
    public class WinPanel : MonoBehaviour
    {
        [SerializeField]
        private GameManager _gameManager;

        void Start()
        {
            _gameManager.Win += ShowPanel;
            gameObject.SetActive(false);
        }

        private void ShowPanel()
        {
            _gameManager.Win -= ShowPanel;
            gameObject.SetActive(true);
        }
        private void OnDestroy()
        {
            if (_gameManager != null)
            {
                _gameManager.Win -= ShowPanel;
            }
        }
    }
}
