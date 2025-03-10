using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace LearnGame.UI
{
    public class OpenLevel : MonoBehaviour
    {
        private Button _openLevelButton;

        [SerializeField]
        private List<int> _levelList = new List<int>();

        private void Start()
        {
            _openLevelButton = GetComponent<Button>();
            _openLevelButton.onClick.RemoveAllListeners();
            _openLevelButton.onClick.AddListener(OpenRandomLevel);
        }

        private void OpenRandomLevel()
        {
            int level = _levelList[Random.Range(0, _levelList.Count)];
            SceneManager.LoadScene(level);
        }
    }
}
