using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class MapSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] _technician = new GameObject[4];
    [SerializeField] private GameObject[] _cameraMan = new GameObject[4];
    [SerializeField] private GameObject[] _guard = new GameObject[4];

    [SerializeField] private GameObject _scissors;
    [SerializeField] private GameObject _weapon;
    [SerializeField] private GameObject _drugs;
    
    private NPCsFlags _currentNPCsFlags;
    private Flags _currentFlags;
    
    [SerializeField]
    private ScreenFade _screenFade;
    
    
    public void UseMap(InputAction.CallbackContext ctx)
    {
        _screenFade.PlayFade();
    }

    public void ToggleMap()
    {
        DungeonMaster.Instance.GetPlayer().GetComponent<PlayerMovement>().ToggleMove();
        this.gameObject.SetActive(!this.gameObject.activeSelf);
        if(this.gameObject.activeSelf)
            UpdateMap();
    }
    
    public void UpdateMap()
    {
        _currentFlags = DungeonMaster.Instance.GetFlags();
        _currentNPCsFlags = DungeonMaster.Instance.GetNPCsFlags();
        updateItems();
        switch (DungeonMaster.Instance.GetCurrentTimePeriod()) //se actualizan tmb los periodos anteriores por si acaso no hubieras mirado el mapa en un periodo anterior que se actualice bien (tmb el switch no dejaba hacer los cases en orden inverso y sin breaks para que ocurrieran en cascada por algun motivo)
        {
            case TimePeriod.Morning:
                updateMorning();
                break;
            case TimePeriod.Afternoon:
                updateMorning();
                updateAfternoon();
                break;
            case TimePeriod.Evening:
                updateMorning();
                updateAfternoon();
                updateEvening();
                break;
            case TimePeriod.EndGame:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void updateMorning()
    {
        //technician
        if (_currentNPCsFlags.HasFlag(NPCsFlags.T_Morning))
        {
            _technician[0].SetActive(true);
        }
        
        //Camera
        if (_currentNPCsFlags.HasFlag(NPCsFlags.C_Morning))
        {
            _cameraMan[0].SetActive(true);
        }
        
        //guard
        if (_currentNPCsFlags.HasFlag(NPCsFlags.G_Morning))
        {
            _guard[0].SetActive(true);
        }
    }
    
    private void updateAfternoon()
    {
        //technician
        if (_currentNPCsFlags.HasFlag(NPCsFlags.T_Afternoon))
        {
            _technician[1].SetActive(true);
        }
        
        //Camera
        if (_currentNPCsFlags.HasFlag(NPCsFlags.C_Afternoon))
        {
            if(_currentFlags.HasFlag(Flags.LightsOut))
                _cameraMan[1].SetActive(true);
            else
                _cameraMan[2].SetActive(true);
        }
        
        //guard
        if (_currentNPCsFlags.HasFlag(NPCsFlags.G_Afternoon))
        {
            _guard[1].SetActive(true);
        }
    }
    
    private void updateEvening()
    {
        //technician
        if (_currentNPCsFlags.HasFlag(NPCsFlags.T_Morning))
        {
            if(_currentFlags.HasFlag(Flags.TechnicianDead))
                _technician[2].SetActive(true);
            else
                _technician[3].SetActive(true);
        }
        
        //Camera
        if (_currentNPCsFlags.HasFlag(NPCsFlags.C_Morning))
        {
            _cameraMan[3].SetActive(true);
        }
        
        //guard
        if (_currentNPCsFlags.HasFlag(NPCsFlags.G_Morning))
        {
            if (_currentFlags.HasFlag(Flags.TeaPrepared) && _currentFlags.HasFlag(Flags.TeaPosioned) && _currentFlags.HasFlag(Flags.DrankTea))
                _guard[3].SetActive(true);
            else 
                _guard[2].SetActive(true);
        }
    }

    private void updateItems()
    {
        _scissors.SetActive(_currentFlags.HasFlag(Flags.HasScissors));
        _weapon.SetActive(_currentFlags.HasFlag(Flags.HasWeapon));
        _drugs.SetActive(_currentFlags.HasFlag(Flags.HasDrugs) && !_currentFlags.HasFlag(Flags.TeaPosioned));
    }
}
