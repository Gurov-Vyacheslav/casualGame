namespace LearnGame.UI
{
    public class WinPanel : BasePanel
    {
        protected override void Start()
        {
            _gameManager.Win += ShowPanel;
            base.Start();
        }

        protected override void ShowPanel()
        {
            _gameManager.Win -= ShowPanel;
            base.ShowPanel();
        }
        protected override void OnDestroy()
        {
            if (_gameManager != null)
            {
                _gameManager.Win -= ShowPanel;
            }
        }
    }
}
