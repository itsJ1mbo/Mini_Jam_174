using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor4000 : MonoBehaviour
{
    private bool _inRange = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("IN RANGE");
        _inRange = true;
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("OUT OF RANGE");
        _inRange = false;
    }
    
    public void OnInteract(InputAction.CallbackContext obj)
    {
        if (_inRange) Debug.Log("INTERACT");
    }
}
