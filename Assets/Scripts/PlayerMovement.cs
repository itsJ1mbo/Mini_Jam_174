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
        _direction = new Vector3(obj.ReadValue<Vector2>().x, obj.ReadValue<Vector2>().y, 0).normalized;
    }

    private bool IsBlocked(Vector3 direction)
    {
        RaycastHit2D hit = Physics2D.CircleCast(_tr.position, 
                                                _sprite.size.x * 0.5f, 
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
