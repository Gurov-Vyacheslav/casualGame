using System.Collections;
using UnityEngine;

namespace LearnGame.UI
{
    public class PausePanel : BasePanel
    {
        protected override void Start()
        {
            _gameManager.PressPause += ShowPanel;
            base.Start();
        }

        protected override void ShowPanel()
        {
            gameObject.SetActive(!gameObject.activeInHierarchy);
        }

        protected override void OnDestroy()
        {
            if (_gameManager != null)
            {
                _gameManager.PressPause -= ShowPanel;
            }
        }
    }
}