namespace LearnGame.UI
{
    public class WinPanel : BasePanel
    {
        protected override void Start()
        {
            GameManager.Instance.Win += ShowPanel;
            base.Start();
        }

        protected override void ShowPanel()
        {
            GameManager.Instance.Win -= ShowPanel;
            base.ShowPanel();
        }
        protected override void OnDestroy()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.Win -= ShowPanel;
            }
        }
    }
}
