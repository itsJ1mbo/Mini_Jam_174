using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 _direction;
    [SerializeField] private float _vel = 5f;
    [SerializeField] private float _checkDistance = 0.1f;
    private bool _canMove = true;

    private Transform _tr;
    [SerializeField] private LayerMask _obstacleLayer;
    private SpriteRenderer _sprite;
    
    // Update is called once per frame
    private void Update()
    {
        if (_canMove && !IsBlocked(_direction))
        {
            _tr.position += _direction * (_vel * Time.deltaTime);
        }
    }

    public void OnMove(InputAction.CallbackContext obj)
    {
        Vector2 input = obj.ReadValue<Vector2>();
    
        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
        {
            _direction = new Vector3(input.x, 0, 0).normalized;
        }
        else
        {
            _direction = new Vector3(0, input.y, 0).normalized;
        }
        
        RotateSprite();
    }
    
    private void RotateSprite()
    {
        if (_direction == Vector3.zero)
            return;

        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 90.0f);
    }

    private bool IsBlocked(Vector3 direction)
    {
        RaycastHit2D hit = Physics2D.CircleCast(_tr.position, 
                                                _sprite.size.x * 0.5f * 0.1f, 
                                                direction,
                                                _checkDistance, 
                                                _obstacleLayer);

        return hit.collider != null;
    }

    public void ToggleMove()
    {
        _canMove = !_canMove;
    }
    
    private void Start()
    {
        _tr = transform;
        _sprite = GetComponent<SpriteRenderer>();
    }
}
