using System;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
[Flags] public enum Flags 
{
    /// <summary>
    /// Son potencias de 2 porque se hacen operaciones a nivel de bit para combinar las diferentes flags.
    /// IMPORTANTE:
    /// No usar "Everything", no aparece definido aqui, pero es algo que Unity hace por defecto,
    /// el motivo es que si el estado del juego se representa asi: 0000 0000 0000 0000 (no es especificamente esa longitud, esto es un ejemplo de como se representa None),
    /// el Everything seria: 1111 1111 1111 1111, cosa que en nuestro caso no es verdad ya que seria 0000 0000 1111 1111
    /// </summary>
    None = 0, //0000 0000
    HasWeapon = 1, //0000 0001
    HasDrugs = 2, //0000 0010
    HasScissors = 4, //me da pereza seguir con el resto la idea se entiende
    TechnicianDead = 8,
    LightsOut = 16,
    CamerasSabotaged = 32,
    TeaPrepared = 64,
    TeaPosioned = 128,
    DrankTea = 256
}

[Flags] public enum NPCsFlags 
{
    None = 0,
    T_Morning = 1,
    T_Afternoon = 2,
    T_Evening = 4,
    C_Morning = 8,
    C_Afternoon = 16,
    C_Evening = 32,
    G_Morning = 64,
    G_Afternoon = 128,
    G_Evening = 256
}

public enum TimePeriod
{
    Morning = 0,
    Afternoon = 1,
    Evening = 2,
    EndGame = 3
}

public class DungeonMaster : MonoBehaviour
{
    private const float MINUTE_LENGTH = 60.0F;

    [SerializeField]
    private UIManager _uiManager;
    
    #region Variables
    public static DungeonMaster Instance { get; private set; }
    /// <summary>
    /// Periodo del dia actual
    /// </summary>
    private TimePeriod _currentTimePeriod = TimePeriod.Morning;
    /// <summary>
    /// Estado actual de las flags del juego
    /// </summary>
    [SerializeField]
    private Flags _currentFlags = Flags.None;
    /// <summary>
    /// Estado actual de las flags del juego
    /// </summary>
    [SerializeField]
    private NPCsFlags _currentNPCsFlags = NPCsFlags.None;
    /// <summary>
    /// Lista con todas las entidades (objetos/personajes) del juego que cambian de estado dependiendo de la etapa del dia
    /// </summary>
    private List<GameObject> _entities = new List<GameObject>();
    /// <summary>
    /// lista de nieblas
    /// </summary>
    private List<GameObject> fogObjects = new List<GameObject>();
    /// <summary>
    /// Duracion de las etapas del juego
    /// </summary>
    [Tooltip("Duración de cada periodo de tiempo del juego en minutos")] 
    [SerializeField] private float _timePeriodLength = 5;
    /// <summary>
    /// Timer interno para las etapas del juego
    /// </summary>
    private float _timePeriodTimer = 0.0f;
    /// <summary>
    /// Booleano que controla si queremos que el tiempo corra o no
    /// </summary>
    private bool _runTimer = false;
    #endregion
    
    #region Functions
    
    #region Unity Callbacks
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        _runTimer = true;
        UpdateGameState();
    }

    void Update()
    {
        if (_runTimer && _currentTimePeriod != TimePeriod.EndGame)
        {
            _timePeriodTimer += Time.deltaTime;
            //Debug.Log(_timePeriodTimer);
            _uiManager.UpdateClock(120/ (_timePeriodLength * 60));
            if(_timePeriodTimer >= _timePeriodLength * MINUTE_LENGTH)
                NextTimePeriod();
        }
    }
    
    #endregion
    
    #region Public Functions
    public UIManager GetUIManager() { return _uiManager; }
    public void SetFlag(Flags flag) { _currentFlags |= flag; }
    public void RemoveFlag (Flags flag) { _currentFlags &= ~flag; }
    public NPCsFlags GetNPCsFlags () { return _currentNPCsFlags; }
    public void SetNPCsFlag(NPCsFlags flag) { _currentNPCsFlags |= flag; }
    public void RemoveNPCsFlag (NPCsFlags flag) { _currentNPCsFlags &= ~flag; }
    public Flags GetFlags () { return _currentFlags; } 
    public void RegisterEntity (GameObject entity) { _entities.Add(entity); }
    public void DeregisterEntity (GameObject entity) { _entities.Remove(entity); }
    public void RegisterFog(GameObject fog) { fogObjects.Add(fog); }
    public void ToggleTimer() { _runTimer = !_runTimer; }
    public TimePeriod GetCurrentTimePeriod () { return _currentTimePeriod; }
    
    public void updateFog(GameObject fogObject)
    {
        foreach (GameObject fog in fogObjects)
        {
            //Debug.Log(fog);
            fog.GetComponent<SpriteRenderer>().enabled = true;
        }
        //Debug.Log(fogObject);
        fogObject.GetComponent<SpriteRenderer>().enabled = false;
    }
    #endregion

    #region Private Functions

    private void NextTimePeriod()
    {
        //hacer una animacion to guapa para mostrar el paso del tiempo
        _currentTimePeriod++;
        if(_currentTimePeriod != TimePeriod.EndGame)
            UpdateGameState();
        _timePeriodTimer = 0.0f;
        /*Debug.Log(_currentTimePeriod);
        Debug.Log(_currentFlags.ToBinaryString());*/
    }
    private void UpdateGameState()
    {
        foreach (GameObject entity in _entities)
        {
            entity.GetComponent<StateHandler>().UpdateState();
        }
    }

    #endregion
    
    #endregion
};
