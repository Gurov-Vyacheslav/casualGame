using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace LearnGame.UI
{
    public class OpenLevel : MonoBehaviour
    {
        private Button openLevelButton;

        [SerializeField]
        private List<int> _levelList = new List<int>();

        private void Awake()
        {
            Time.timeScale = 1;
        }

        private void Start()
        {
            openLevelButton = GetComponent<Button>();
            openLevelButton.onClick.RemoveAllListeners();
            openLevelButton.onClick.AddListener(OpenRandomLevel);
        }

        private void OpenRandomLevel()
        {
            int level = _levelList[Random.Range(0, _levelList.Count)];
            SceneManager.LoadScene(level);
        }
    }
}
