using System;
using System.Collections;
using Elevator.Display;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public event Action OnPassengersChanged;
    public event Action OnMoveCounterChanged;
    
    private UnityDisplay _unityDisplay;
    
    private int _numberOfPassengers;

    private int _moveCounter;

    private bool _initialized;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        StartCoroutine(WaitForUnityDisplayInitialization());
    }

    private IEnumerator WaitForUnityDisplayInitialization()
    {
        while (_unityDisplay == null)
        {
            _unityDisplay = FindObjectOfType<UnityDisplay>();
            yield return null;
        }
        
        _numberOfPassengers = _unityDisplay.numberOfPersons;
        Debug.LogError($"Number {_numberOfPassengers}");
        _initialized = true;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(WaitForUnityDisplayInitialization());
    }

    public void PassengersCounter()
    {
        if (!_initialized)
        {
            return;
        }
        
        
        if (_numberOfPassengers > 1)
        {
            _numberOfPassengers -= 1;
            _unityDisplay.numberOfPersons = _numberOfPassengers;
            Debug.LogError($"HERE IN COUNT {_unityDisplay.numberOfPersons}");
            OnPassengersChanged?.Invoke();
        }
        else
        {
            StartCoroutine(WaitForPerson());
        }
    }

    public IEnumerator WaitForPerson()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("EndGame");
    }

    public void MoveCounter()
    {
        _moveCounter += 1;
        OnMoveCounterChanged?.Invoke();
    }

    public int GetPassengers()
    {
        return _numberOfPassengers;
    }

    public int GetMoveCounter()
    {
        return _moveCounter;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
