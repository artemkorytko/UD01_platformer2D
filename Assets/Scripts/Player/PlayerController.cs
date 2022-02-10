using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _horizontalSpeed = 5f;
    [SerializeField] private float _jumpImpulse = 5f;

    private Rigidbody2D _rigidBody = null;
    private Health _health = null;
    private PlayerAnimationController _animationController = null;
    private bool _isActive = true;
    private bool _isCanJump = true;
    private float _maxVelocityMagnitude = 0f;

    public System.Action OnDead = null;
    public System.Action OnWin = null;
    public System.Action OnCoinCollected = null;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _health = GetComponent<Health>();
        _animationController = GetComponent<PlayerAnimationController>();

        _maxVelocityMagnitude = Mathf.Sqrt(Mathf.Pow(_jumpImpulse, 2f) + Mathf.Pow(_horizontalSpeed, 2f));
        _health.OnDie += OnDie;
    }

    private void OnDisable()
    {
        _health.OnDie -= OnDie;
    }

    private void Update()
    {
        if (!_isActive) return;

        Movement();
        UpdateSide();
    }

    private void Movement()
    {
        HorizontalMovement();
        VerticalMovement();
        ClampVelocity();
    }

    private void HorizontalMovement()
    {
        float horizontalAxis = SimpleInput.GetAxis("Horizontal");
        Vector3 velocity = _rigidBody.velocity;
        velocity.x = horizontalAxis * _horizontalSpeed;
        _rigidBody.velocity = velocity;
        _animationController.SetSpeedDirection(velocity.x == 0 ? 0 : (int)Mathf.Sign(velocity.x));
    }

    private void VerticalMovement()
    {
        if (_isCanJump && SimpleInput.GetAxis("Vertical") > 0)
        {
            _rigidBody.AddForce(_jumpImpulse * Vector3.up, ForceMode2D.Impulse);
            _animationController.SetJump();
            _isCanJump = false;
        }
    }

    private void ClampVelocity()
    {
        float velocityMagnitude = _rigidBody.velocity.magnitude;
        velocityMagnitude = Mathf.Clamp(velocityMagnitude, 0f, _maxVelocityMagnitude);
        _rigidBody.velocity = _rigidBody.velocity.normalized * velocityMagnitude;
    }

    private void UpdateSide()
    {
        bool isNeedUpdate = Mathf.Abs(_rigidBody.velocity.x) > 0f ? true : false;
        if (!isNeedUpdate) return;

        float side = Mathf.Sign(_rigidBody.velocity.x);
        Vector3 localScale = transform.localScale;
        if (Mathf.Sign(localScale.x) != side)
        {
            localScale.x *= -1f;
        }

        transform.localScale = localScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_isActive) return;

        PlatformController platform = collision.gameObject.GetComponent<PlatformController>();

        if (platform != null)
        {
            _isCanJump = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isActive) return;

        Coin coin = collision.gameObject.GetComponent<Coin>();

        if (coin != null)
        {
            coin.gameObject.SetActive(false);
            OnCoinCollected?.Invoke();
        }

        Finish finish = collision.GetComponent<Finish>();
        if (finish != null)
        {
            Deactivate();
            _animationController.SetSpeedDirection(0);
            StartCoroutine(DelayCall(OnWin, 2f));
        }
    }

    private void OnDie()
    {
        Deactivate();
        _animationController.SetDead();
        StartCoroutine(DelayCall(OnDead, 2f));
    }

    private void Deactivate()
    {
        _isActive = false;
        _rigidBody.velocity = Vector3.zero;
        _health.OnDie -= OnDie;
    }

    private IEnumerator DelayCall(System.Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }
}
