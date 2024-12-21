using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor4000 : MonoBehaviour
{
    private bool _inRange = false;
    private GameObject _obj;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("IN RANGE");
        _inRange = true;
        _obj = other.gameObject;
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("OUT OF RANGE");
        _inRange = false;
        _obj = null;
    }
    
    public void OnInteract(InputAction.CallbackContext ctx)
    {
        Debug.Log("Gfsfs");
        if (_inRange)
        {
            Debug.Log("INTERACT");
            if (_obj.gameObject.TryGetComponent<Guion>(out Guion g))
            {
                g.NextLine();
            }
            if(!_obj.GetComponent<Guion>() && _obj.GetComponent<FlagInteraction>())
                _obj.GetComponent<FlagInteraction>().activateFlag();
        }
    }
}
