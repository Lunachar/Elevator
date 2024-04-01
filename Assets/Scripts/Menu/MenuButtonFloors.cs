using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Elevator {
public class MenuButtonFloors : MonoBehaviour
{
    public GameObject ButtonPrefab;
    
    private Boot _boot;

    // [Inject]
    // public void Construct(Boot boot)
    // {
    //     _boot = boot;
    // }
    
    void Start()
    {
        _boot = GameObject.FindObjectOfType<Boot>().GetComponent<Boot>();
        buttonsInitialize();

    }

    
    void Update()
    {

    }

    private void buttonsInitialize()
    {
        float buttonStartPositionX = 0f; 
        float buttonStartPositionY = 0f;

        if (_boot != null)
        {
            for (int button = 0; button < _boot.GetNumberOfFloors(); button++) // 0 to _boot.GetNumberOfFloors()
            {
                GameObject buttonObject = Instantiate(ButtonPrefab, new Vector3(buttonStartPositionX, buttonStartPositionY), Quaternion.identity, transform);
                buttonObject.name = "Floor " + button;
                
               

                buttonStartPositionX += 1f;
            }
        }
        else
        {
            Debug.LogError("_boot is null");
        }
    }
}
}
