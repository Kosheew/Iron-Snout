using CustomEventBus;
using CustomEventBus.Signals;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Level
{
    public class UserInterface : MonoBehaviour, IService
    {
        [SerializeField] private Text _healthText;
        [SerializeField] private Text _scoreText;

        private EventBus _eventBus;

        public void Init()
        {
            _eventBus = ServiceLocator.Current.Get<EventBus>();
            _eventBus.Subscribe<OnHealthChange>(ChangeHealth);
            _eventBus.Subscribe<OnScoreChange>(ScoreChange);
        }

        private void ChangeHealth(OnHealthChange signal)
        {
            _healthText.text = $"{signal.Value}";
        }

        private void ScoreChange(OnScoreChange signal)
        {
            _scoreText.text = $"{signal.Value}";
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<OnHealthChange>(ChangeHealth);
            _eventBus.Unsubscribe<OnScoreChange>(ScoreChange);
        }
    }
}