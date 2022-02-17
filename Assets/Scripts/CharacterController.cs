using System;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterController : MonoBehaviour
{
    public event Action OnWin;
    public event Action OnFail;
    public event Action OnCoinsCollected;

    [SerializeField] private float horizontalSpeed = 5f;
    [SerializeField] private float jumpImpulse = 5f;
    [SerializeField] private float gravity = -9.81f;

    [Space, SerializeField]
    private TouchChecker _groundChecker;

    private Rigidbody2D _rigidbody;
    private CharacterAnimationController _animationController;
    private Health _health;

    private bool _isActive = true; //TODO
    private bool _isGrounded;
    private float _maxVelocityMagnitude;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _health = GetComponent<Health>();
        _animationController = GetComponent<CharacterAnimationController>();
    }

    private void Start()
    {
        _health.OnDie += OnDie;
        _groundChecker.OnTouch += TouchStateUpdate;
        _maxVelocityMagnitude = Mathf.Sqrt(Mathf.Pow(jumpImpulse, 2) + Mathf.Pow(horizontalSpeed, 2));
    }

    private void OnDestroy()
    {
        _health.OnDie -= OnDie;
        _groundChecker.OnTouch -= TouchStateUpdate;
    }

    private void FixedUpdate()
    {
        if (!_isActive) return;

        Move();
        UpdateSide();
    }

    private void Move()
    {
        HorizontalMove();
        VerticalMove();
        ClampVelocity();
    }

    private void TouchStateUpdate(bool state, GameObject other)
    {
        if (!_isActive) return;
        switch (other.tag)
        {
            case  "Ground" :
                _isGrounded = state;
                _animationController.SetGrounded(_isGrounded);
                break;
            case "Enemy" :
                var velocity = _rigidbody.velocity;
                velocity.y += Mathf.Sqrt(jumpImpulse * -3f * gravity);
                _rigidbody.velocity = velocity;
                break;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_isActive) return;
    
        var coin = other.gameObject.GetComponent<Coin>();
        if (coin != null)
        {
            coin.gameObject.SetActive(false);
            OnCoinsCollected?.Invoke();
        }
        
        var finish = other.gameObject.GetComponent<Finish>();
        if (finish != null)
        {
            Deactivate();
            finish.gameObject.SetActive(false);
            OnWin?.Invoke();
        }
    }

    private void Deactivate()
    {
        _isActive = false;
        _animationController.SetGrounded(true);
        _rigidbody.velocity = Vector2.zero;
    }

    private void HorizontalMove()
    {
        var horizontalAxis = SimpleInput.GetAxis("Horizontal");
        var velocity = _rigidbody.velocity;

        velocity.x = horizontalAxis * horizontalSpeed;
        _rigidbody.velocity = velocity;
        
        _animationController.SetSpeed(velocity.x == 0 ? 0 : (int) Mathf.Sign(velocity.x));
    }
    
    private void VerticalMove()
    {
        var velocity = _rigidbody.velocity;
        
        if (_isGrounded && SimpleInput.GetAxis("Vertical") > 0)
        {
            _isGrounded = false;
            velocity.y += Mathf.Sqrt(jumpImpulse * -3f * gravity);
        }
        
        if (!_isGrounded)
            velocity.y += gravity * Time.deltaTime;
        
        _rigidbody.velocity = velocity;
        _animationController.SetVerticalSpeed(_rigidbody.velocity.y == 0 ? 0 : (int) Mathf.Sign(_rigidbody.velocity.y));
    }

    private void ClampVelocity()
    {
        var velocity = _rigidbody.velocity.magnitude;
        velocity = Mathf.Clamp(velocity, 0, _maxVelocityMagnitude);
        _rigidbody.velocity = _rigidbody.velocity.normalized * velocity;
    }

    private void UpdateSide()
    {
        var isNeedUpdate = Mathf.Abs(_rigidbody.velocity.x) > 0f;
        if (!isNeedUpdate) return;

        var side = Mathf.Sign(_rigidbody.velocity.x);
        var localScale = transform.localScale;
        if (Mathf.Sign(localScale.x) != side)
            localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDie()
    {
        Deactivate();
        OnFail?.Invoke();
    }

    
}
