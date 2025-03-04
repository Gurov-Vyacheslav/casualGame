using LearnGame.UI;

namespace LearnGame.Ui
{
    public class LosePanel : BasePanel
    {
        protected override void Start()
        {
            _gameManager.Loss += ShowPanel;
            base.Start();
        }

        protected override void ShowPanel()
        {
            _gameManager.Loss -= ShowPanel;
             base.ShowPanel();
        }

        protected override void OnDestroy()
        {
            if (_gameManager != null)
            {
                _gameManager.Loss -= ShowPanel;
            }
        }
    }
}
