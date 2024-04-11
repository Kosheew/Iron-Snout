using UnityEngine;

namespace Game.Characters
{
    public class CharacterAnim : MonoBehaviour, IAnim
    {
        private const string TriggerAnimHit = "Hit";
        private Animator _animator;

        void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void TakeAnim()
        {
            _animator.SetTrigger(TriggerAnimHit);
        }
    }
}