using System.Collections;
using UnityEngine;

namespace LearnGame.UI
{
    public abstract class BasePanel : MonoBehaviour
    {

        [SerializeField]
        protected GameManager _gameManager;

        protected virtual void Start()
        {
            gameObject.SetActive(false);
        }

        protected virtual void ShowPanel()
        {
            gameObject.SetActive(true);
        }

        protected abstract void OnDestroy();
    }
}