using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 5f;
    [SerializeField] private float jumpImpulse = 10f;

    private Rigidbody2D rigidbody;
    private Health health;
    private PlayerAnimationController playerAnimationController;

    private bool isActive = true;
    private bool isCanJump;
    private float maxVelocityMagnitude;
    
    public event Action OnWin;
    public event Action OnLost;
    public event Action OnCoinCollected;
   
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        playerAnimationController = GetComponent<PlayerAnimationController>();
        
    }

    private void Start()
    {
        health.OnDie += OnDie;
        maxVelocityMagnitude = Mathf.Sqrt(Mathf.Pow(jumpImpulse, 2) + Mathf.Pow(horizontalSpeed, 2));
        
    }

    private void FixedUpdate()
    {
        if(!isActive)
            return;
        
        Movement();
        UpdateSide();
    }
    private void Movement()
    {
        HorizontalMove();
        VerticalMove();
        ClampVelocity();
    }

    private void HorizontalMove()
    {
       var horizontalAxis = SimpleInput.GetAxis("Horizontal");
       var velocity = rigidbody.velocity;
       velocity.x = horizontalAxis * horizontalSpeed;
       rigidbody.velocity = velocity;
       playerAnimationController.SetSpeed(velocity.x == 0 ? 0 : (int)Mathf.Sign(velocity.x));
    }

    private void VerticalMove()
    {
        if (isCanJump && SimpleInput.GetAxis("Vertical") > 0)
        {
            isCanJump = false;
            rigidbody.AddForce(Vector2.up * jumpImpulse,ForceMode2D.Impulse);
            playerAnimationController.SetJump();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(!isActive)
            return;
        
        if (col.gameObject.CompareTag("Ground"))
        {
            isCanJump = true;
        }
        if (col.gameObject.CompareTag("Fell"))
        {
            OnDie();
        }
        if (col.gameObject.CompareTag("LethalZone"))
        {
            Debug.Log("LethalZone trigger");
        }
      
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(!isActive)
            return;

        var coin = col.gameObject.GetComponent<Coin>();
        if (coin != null)
        {
            coin.gameObject.SetActive(false);
            OnCoinCollected?.Invoke();
        }
        var heart = col.gameObject.GetComponent<Heart>();
        if (heart != null)
        {
            heart.gameObject.SetActive(false);
            health.CurrentValue++;
        }
        
        if (col.gameObject.CompareTag("LethalZone"))
        {
            var enemy = col.gameObject.GetComponentInParent<EnemyController>();
            enemy.gameObject.SetActive(false);
        }
     
        
        var finish = col.gameObject.GetComponent<Finish>();
        if (finish != null)
        {
            Deactivate();
            playerAnimationController.SetSpeed(0);
            OnWin?.Invoke();
           
        }
    }

    private void Deactivate()
    {
        isActive = false;
        rigidbody.velocity = Vector2.zero;
    }

    private void ClampVelocity()
    {
        var velocity = rigidbody.velocity.magnitude;
        velocity = Mathf.Clamp(velocity,0,maxVelocityMagnitude);
        rigidbody.velocity = rigidbody.velocity.normalized * velocity;
    }

    private void UpdateSide()
    {
        var isNeedUpdate = Mathf.Abs(rigidbody.velocity.x) > 0;
        if(!isNeedUpdate)
            return;
        var side = Mathf.Sign(rigidbody.velocity.x);
        var localScale = transform.localScale;
        if (Mathf.Sign(localScale.x) != side)
        {
            localScale.x *= -1;
        }

        transform.localScale = localScale;
    }
    
    private void OnDestroy()
    {
        health.OnDie -= OnDie;
    }

   
    private void OnDie()
    {
        Deactivate();
        OnLost?.Invoke();
    }
}
