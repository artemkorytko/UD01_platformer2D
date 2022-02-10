using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float damping = 1.5f;
    [SerializeField] private Transform _target = null;
    [SerializeField] private Vector2 offset = new Vector2(2f, 1f);

    private bool _faceLeft;
    private int _lastX;
    private float _dynamicSpeed;
    private Camera _camera;

    public void Initialize(Transform target)
    {
        _target = target;
        FindPlayer();
    }

    void Start()
    {
        offset = new Vector2(Mathf.Abs(offset.x), offset.y);
        _camera = gameObject.GetComponent<Camera>();
    }

    public void FindPlayer()
    {
        _lastX = Mathf.RoundToInt(_target.position.x);
        transform.position = new Vector3(_target.position.x + offset.x, _target.position.y + offset.y, transform.position.z);
    }

    void FixedUpdate()
    {
        if (_target != null)
        {
            int currentX = Mathf.RoundToInt(_target.position.x);
            if (currentX > _lastX) _faceLeft = false; else if (currentX < _lastX) _faceLeft = true;
            _lastX = Mathf.RoundToInt(_target.position.x);

            Vector3 target;
            if (_faceLeft)
            {
                target = new Vector3(_target.position.x - offset.x, _target.position.y + offset.y + _dynamicSpeed, transform.position.z);
            }
            else
            {
                target = new Vector3(_target.position.x + offset.x, _target.position.y + offset.y + _dynamicSpeed, transform.position.z);
            }
            Vector3 currentPosition = Vector3.Lerp(transform.position, target, damping * Time.deltaTime);
            transform.position = currentPosition;
        }
    }
}
