using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LearnGame.Ui
{
    public class CounterEnemyUi : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _outputText;

        private int _currentCountEnemy;

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
