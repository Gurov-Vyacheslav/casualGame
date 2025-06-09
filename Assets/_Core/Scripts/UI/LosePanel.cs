namespace LearnGame.UI
{
    public class LosePanel : BasePanel
    {
        protected override void Start()
        {
            GameManager.Instance.Loss += ShowPanel;
            base.Start();
        }

        protected override void ShowPanel()
        {
            GameManager.Instance.Loss -= ShowPanel;
             base.ShowPanel();
        }

        protected override void OnDestroy()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.Loss -= ShowPanel;
            }
        }
    }
}
