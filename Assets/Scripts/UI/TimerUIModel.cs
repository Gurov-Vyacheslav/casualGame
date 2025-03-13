using LearnGame.Timer;
using UnityEngine;

namespace LearnGame.UI
{
    public class TimerUIModel
    {

        private ITimer _timer;
        private float _gameDurationSeconds;
        private float _timerSeconds;

        public bool Finish => _timerSeconds >= _gameDurationSeconds;
        public TimerUIModel(ITimer timer, float gameDurationSeconds) 
        {
            _timer = timer;
            _gameDurationSeconds = gameDurationSeconds;
            _timerSeconds = 0;
        }

        public int UpdateTimer()
        {
            _timerSeconds += _timer.DeltaTime;
            int time = (int)(_gameDurationSeconds - _timerSeconds);
            return time;
        }
    }
}
