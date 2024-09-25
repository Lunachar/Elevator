using System.Collections;
using System.Collections.Generic;
using Elevator.Display;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameManager : MonoBehaviour
{
    private UnityDisplay _unityDisplay;
    
    private int NumberOfPassengers;

    [Inject]
    public void Construct(UnityDisplay unityDisplay)
    {
        _unityDisplay = unityDisplay;
    }
    void Start()
    {
    }

    public void PassengersCounter()
    {
        // if (!_unityDisplay)
        // {
        //     _unityDisplay = FindObjectOfType<UnityDisplay>();
        // }
        Debug.LogError("HERE IN COUNT");
        NumberOfPassengers = _unityDisplay.numberOfPersons;
        if (NumberOfPassengers > 0)
        {
            NumberOfPassengers -= 1;
            _unityDisplay.numberOfPersons = NumberOfPassengers;
            Debug.LogError($"Number of passengers: {_unityDisplay.numberOfPersons}");
        }
        else
        {
            Debug.LogError("EndGame");
            SceneManager.LoadScene("EndGame");
        }

    }
}
