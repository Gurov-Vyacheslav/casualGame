namespace LearnGame.UI
{
    public class PausePanel : BasePanel
    {
        protected override void Start()
        {
            GameManager.Instance.PressPause += ShowPanel;
            base.Start();
        }

        protected override void ShowPanel()
        {
            gameObject.SetActive(!gameObject.activeInHierarchy);
        }

        protected override void OnDestroy()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.PressPause -= ShowPanel;
            }
        }
    }
}