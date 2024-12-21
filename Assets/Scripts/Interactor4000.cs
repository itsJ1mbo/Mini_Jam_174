using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor4000 : MonoBehaviour
{
    [SerializeField] private UIManager _ui;
    
    private bool _inRange = false;
    private GameObject _obj;

    private void OnTriggerEnter2D(Collider2D other)
    {
        _inRange = true;
        _obj = other.gameObject;
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        _inRange = false;
        _obj = null;
    }
    
    public void OnInteract(InputAction.CallbackContext ctx)
    {
        if (_inRange)
        {
            if (_obj.gameObject.TryGetComponent<Guion>(out Guion g))
                Dialogue(g);
            if(!_obj.GetComponent<Guion>() && _obj.GetComponent<FlagInteraction>())
                _obj.GetComponent<FlagInteraction>().activateFlag();
        }
    }

    private void Dialogue(Guion g)
    {
        if (g.Talking)
        {
            if (_ui.Typing) _ui.Skip();
            else g.NextLine();
        }
        else
        {
            GetComponent<PlayerMovement>().ToggleMove();
            g.StartDialogue();
        }
    }
}
