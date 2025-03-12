using UnityEngine;
using System;
using TMPro;

namespace LearnGame.UI
{
    public class TimerUI : MonoBehaviour, IInitializerGameManeger
    {
        public event Action TimeEnd;

        [SerializeField]
        private TextMeshProUGUI _outputText;
        private string _format;

        [field:SerializeField]
        public float GameDurationSeconds {  get; private set; }

        public float TimerSeconds { get; private set; }

        private bool _timerEnd;

        public void InitializeGameManager()
        {
            GameManager.Instance.SetTimerUI(this);
        }

        private void Start()
        {
            InitializeGameManager();
            _format = _outputText.text;
            _timerEnd = false;
        }

        private void Update()
        {
            if (_timerEnd) return;

            TimerSeconds += Time.deltaTime;
            if (TimerSeconds >= GameDurationSeconds)
            {
                TimeEnd?.Invoke();
                _timerEnd = true;
            }

            int time = (int)(GameDurationSeconds - TimerSeconds);
            _outputText.text = string.Format(_format, time / 60, time%60);
        }
    }
}
