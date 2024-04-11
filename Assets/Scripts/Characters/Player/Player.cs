using CustomEventBus.Signals;
using CustomEventBus;
using UnityEngine;

namespace Game.Characters
{
    public class Player : MonoBehaviour, IService, IDamageable
    {
        [SerializeField] private int _health = 10;
        private EventBus _eventBus;

        public void Init()
        {
            _eventBus = ServiceLocator.Current.Get<EventBus>();
            _eventBus?.Invoke(new OnHealthChange(_health));
        }

        public void TakeDamage()
        {
            _health--;

            if (_health <= 0)
            {
                _health = 0;
            }

            if (_health == 0)
            {
                _eventBus?.Invoke(new OnGameLoose());
            }
            _eventBus?.Invoke(new OnHealthChange(_health));
        }
    }
}