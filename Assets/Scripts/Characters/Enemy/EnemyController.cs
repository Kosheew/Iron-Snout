using System.Collections;
using UnityEngine;

namespace Game.Characters
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float _enemySpeed = 5f;
        [SerializeField] private float _minDistance = 2f;
        [SerializeField] private float _restartAttackTime = 1f;

        private Transform _target;
        private SpriteRenderer _spriteRenderer;
        private Vector2 _direction;
        private Vector3 _targetPosition;
        private float _distance;
        private float _rayLen = 2f;
        private int _numberLayers = (1 << 6);
        private bool _isPursuing = true;
        private bool _isAttack = false;

        private void Start()
        {
            _target = GameObject.FindGameObjectWithTag(StringConstants.PLAYER).transform;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _distance = Mathf.Abs(transform.position.x - _target.position.x);
            _direction = (_target.position - transform.position).normalized;
            _targetPosition = new Vector3(_target.position.x, transform.position.y, transform.position.z);
            _spriteRenderer.flipX = _direction.x > 0;
        }

        private void FixedUpdate()
        {
            if (_target != null && _isPursuing)
            {
                Movement();
            } 
            if (_isAttack)
            {
                StartCoroutine(Attack());
            }
        }

        private void Movement()
        {
            _distance = Mathf.Abs(transform.position.x - _target.position.x);

            if (_distance > _minDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _enemySpeed * Time.deltaTime);
            }
            else
            {
                if (_distance <= _minDistance)
                {
                    _isPursuing = false;
                    _isAttack = true;
                }
                else
                {
                    _isPursuing = true;
                    _isAttack = false;
                }
            }   
        }

        IEnumerator Attack()
        {
            Vector2 direction = (_target.position - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, _rayLen, _numberLayers);

            if (hit.collider != null)
            {
                IDamageable takeDamage = hit.collider.gameObject.GetComponent<IDamageable>();
                IAnim anim = hit.collider.gameObject.GetComponent<IAnim>();
                if (takeDamage != null && anim != null)
                {
                    anim.TakeAnim();
                    takeDamage.TakeDamage();
                }
            }
            _isAttack = false;
            yield return new WaitForSeconds(_restartAttackTime);
            _isAttack = true;
        }
    }
}