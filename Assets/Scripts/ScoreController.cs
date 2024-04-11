using CustomEventBus;
using CustomEventBus.Signals;
using System;
using UnityEngine;

namespace Game.Level
{
    public class ScoreController : IService, IDisposable
    {
        private EventBus _eventBus;
        private int _score;

        public void Init()
        {
            _eventBus = ServiceLocator.Current.Get<EventBus>();
            _eventBus.Subscribe<AddScoreSignal>(OnScoreAdded);
        }

        private void OnScoreAdded(AddScoreSignal signal)
        {
            _score += signal.Value;
            _eventBus.Invoke(new OnScoreChange(_score));
        }
        public void Dispose()
        {
            _eventBus.Unsubscribe<AddScoreSignal>(OnScoreAdded);
        }
    }
}