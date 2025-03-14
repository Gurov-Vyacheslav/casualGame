using UnityEngine;

namespace LearnGame.UI
{
    public abstract class BasePanel : MonoBehaviour
    {
        protected virtual void Start()
        {
            gameObject.SetActive(false);
        }

        protected virtual void ShowPanel()
        {
            if (!ReferenceEquals(gameObject, null))
                gameObject.SetActive(true);
        }

        protected abstract void OnDestroy();
    }
}