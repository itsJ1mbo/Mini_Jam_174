using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    
    private InputSystem_Actions _input;
    
    private PlayerMovement _playerMovement;
    private Interactor4000 _interactor;

    [SerializeField] private MapSystem _map;
    
    private void OnEnable()
    {
        _input.Player.Move.performed += _playerMovement.OnMove;
        _input.Player.Move.canceled += _playerMovement.OnMove;

        _input.Player.Interact.performed += _interactor.OnInteract;

        _input.Player.Map.performed += _map.UseMap;
        
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Player.Move.performed -= _playerMovement.OnMove;
        _input.Player.Move.canceled -= _playerMovement.OnMove;
        
        _input.Player.Interact.performed -= _interactor.OnInteract;
        
        _input.Player.Map.performed -= _map.UseMap;
        
        _input.Disable();
    }

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _interactor = GetComponent<Interactor4000>();
        
        _input = new InputSystem_Actions();
    }
}
