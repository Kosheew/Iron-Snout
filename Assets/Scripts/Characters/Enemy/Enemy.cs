using CustomEventBus;
using CustomEventBus.Signals;
using UnityEngine;

namespace Game.Characters
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        [SerializeField] private int _myPrice = 1;
        private Vector2 _startPos;
        private EventBus _eventBus;

        public void Start()
        {
            _eventBus = ServiceLocator.Current.Get<EventBus>();
            _startPos = transform.position;
        }

        public void TakeDamage()
        { 
            _eventBus?.Invoke(new AddScoreSignal(_myPrice));

            if ((Vector2)transform.position != _startPos)
            {
                transform.position = _startPos;
                gameObject.SetActive(false);
            }
        }
    }
}
