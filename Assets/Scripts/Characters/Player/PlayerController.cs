using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Game.Characters
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        private const string HorizontalAxis = "Horizontal";
        private const string VerticalAxis = "Vertical";
        private const string Ground = "Ground";
        private const int Axis = 1;
        private const float AttackDelay = 0.35f;

        [SerializeField] private float _jumpSpeed = 8f;

        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rb2d;
        private float _rayLen = 2.5f;
        private int _numberLayers = (1 << 7);
        private bool _canAttack = true;
        private bool _canJump = true;

        public void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rb2d = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            AttackController();
            Jump();
        }

        private void Jump()
        {
            if (_canJump)
            {
                float vertical = Input.GetAxisRaw(VerticalAxis);
                if (vertical >= Axis)
                {
                    _rb2d.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
                    _canJump = false;
                }
            }
        }

        private void AttackController()
        {
            if (_canAttack)
            {
                float horizontal = Input.GetAxisRaw(HorizontalAxis);
                if (horizontal <= -Axis)
                { 
                    _spriteRenderer.flipX = true;
                    StartCoroutine(Attack(-Axis));
                }
                else if (horizontal >= Axis)
                {   
                    _spriteRenderer.flipX = false;
                    StartCoroutine(Attack(Axis));
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(Ground))
            {
                _canJump = true;
            }
        }

        IEnumerator Attack(int dir)
        {
            Vector2 direction = dir * Vector2.right;
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
            _canAttack = false;
            yield return new WaitForSeconds(AttackDelay);
            _canAttack = true;
        }
    }
}