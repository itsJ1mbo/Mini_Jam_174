using System;
using System.Collections.Generic;
using System.Numerics;
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
public class DungeonMaster : MonoBehaviour
{
    #region Variables
    public DungeonMaster Instance { get; private set; }
    
    private Flags _currentFlags = Flags.None;
    private List<GameObject> _characters = new List<GameObject>();
    
    [Tooltip("Duración de cada periodo de tiempo del juego en minutos")]
    [SerializeField] private int _timePeriodLength = 5;
    private float _timePeriodTimer = 0.0f;
    #endregion
    
    #region Functions
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateGameState();
    }

    void Update()
    {
        
    }
    
    public void SetFlag(Flags flag) { _currentFlags |= flag; }
    public void RemoveFlag (Flags flag) { _currentFlags &= ~flag; }
    public void AddCharacter (GameObject character) { _characters.Add(character); }
    public void RemoveCharacter (GameObject character) { _characters.Remove(character); }
    private void UpdateGameState()
    {
        foreach (GameObject character in _characters)
        {
            //Llamar a la cosa que actualice a cada personaje
            //character.GetComponent<>().updateState(_currentFlags);
        }
    }
    #endregion
};
