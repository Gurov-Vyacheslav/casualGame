using LearnGame.Boosters;
using LearnGame.Enemy;
using LearnGame.Timer;
using UnityEngine;
using LearnGame.Spawners;
using LearnGame.Enemy.States;
using LearnGame.UI;

namespace LearnGame.CompositionRoot
{
    [RequireComponent(typeof(TimerUIView))]
    public class TimerUICompositionRoot : CompositionRoot<TimerUIView>
    {
        [SerializeField]
        private float _gameDurationSeconds = 90f;

        private TimerUIView _view;

        public override TimerUIView Compose(ITimer timer)
        {
            _view = GetComponent<TimerUIView>();
            var timerUI = new TimerUIModel(timer, _gameDurationSeconds);
            _view.Initialize(timerUI);

            return _view;
        }

        protected void Awake()
        {
            Compose(GameManager.Instance.Timer);
        }
    }
}
