using TMPro;
using UnityEngine;

namespace LearnGame.UI
{
    public class CounterEnemyUI : MonoBehaviour, IInitializerGameManeger
    {
        [SerializeField]
        private TextMeshProUGUI _outputText;

        private int _currentCountEnemy;

        public void InitializeGameManager()
        {
            GameManager.Instance.SetCounterEnemy(this);
        }

        public void Start()
        {
            InitializeGameManager();
        }

        public void SetMaxCountEnemy(int count)
        {
            _currentCountEnemy = count;
            _outputText.text = _currentCountEnemy.ToString();
        }

        public void KilEnemy()
        {
            _currentCountEnemy--;
            _outputText.text = _currentCountEnemy.ToString();
        }

    }
}
