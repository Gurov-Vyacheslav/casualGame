using UnityEngine;
using System;
using TMPro;

namespace LearnGame.UI
{
    public class TimerUIView : MonoBehaviour, IInitializerGameManeger
    {
        public event Action TimeEnd;

        [SerializeField]
        private TextMeshProUGUI _outputText;

        private string _format;

        private TimerUIModel _model;

        private bool _timerEnd;


        public void Initialize(TimerUIModel model)
        {
            _model = model;
        }
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

            var time = _model.UpdateTimer();
            if (_model.Finish)
            {
                TimeEnd?.Invoke();
                _timerEnd = true;
            }
            _outputText.text = string.Format(_format, time / 60, time%60);
        }
    }
}
