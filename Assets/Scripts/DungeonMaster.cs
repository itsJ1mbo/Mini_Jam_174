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
    DrankTea = 128
}

public enum TimePeriod
{
    Morning = 0,
    Afternoon = 1,
    Evening = 2
}

public class DungeonMaster : MonoBehaviour
{
    private const float MINUTE_LENGTH = 60.0F;

    #region Variables
    public static DungeonMaster Instance { get; private set; }
    /// <summary>
    /// Periodo del dia actual
    /// </summary>
    private TimePeriod _currentTimePeriod = TimePeriod.Morning;
    /// <summary>
    /// Estado actual de las flags del juego
    /// </summary>
    private Flags _currentFlags = Flags.None;
    /// <summary>
    /// Lista con todas las entidades (objetos/personajes) del juego que cambian de estado dependiendo de la etapa del dia
    /// </summary>
    private List<GameObject> _entities = new List<GameObject>();
    /// <summary>
    /// Duracion de las etapas del juego
    /// </summary>
    [Tooltip("Duración de cada periodo de tiempo del juego en minutos")] 
    [SerializeField] private int _timePeriodLength = 5;
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
        if (Instance != null && Instance != this)
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
        if (_runTimer)
        {
            _timePeriodTimer += Time.deltaTime;
            //Debug.Log(_timePeriodTimer);
            if(_timePeriodTimer >= _timePeriodLength * MINUTE_LENGTH)
                NextTimePeriod();
        }
    }
    
    #endregion
    
    #region Public Functions

    public void SetFlag(Flags flag)
    {
        Debug.Log(_currentFlags.ToBinaryString());
        _currentFlags |= flag;
        Debug.Log(_currentFlags.ToBinaryString());
    }
    public void RemoveFlag (Flags flag) { _currentFlags &= ~flag; }
    public void AddEntity (GameObject entity) { _entities.Add(entity); }
    public void RemoveEntity (GameObject entity) { _entities.Remove(entity); }
    public void ToggleTimer() { _runTimer = !_runTimer; }
    #endregion

    #region Private Functions

    private void NextTimePeriod()
    {
        //hacer una animacion to guapa para mostrar el paso del tiempo
        _currentTimePeriod++;
        UpdateGameState();
        _timePeriodTimer = 0.0f;
        /*Debug.Log(_currentTimePeriod);
        Debug.Log(_currentFlags.ToBinaryString());*/
    }
    private void UpdateGameState()
    {
        foreach (GameObject entity in _entities)
        {
            //Llamar a la cosa que actualice a cada personaje
            //entity.GetComponent<>().updateState(_currentTimePeriod, _currentFlags);
        }
    }

    #endregion
    
    #endregion
};
