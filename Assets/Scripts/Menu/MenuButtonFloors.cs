using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Elevator {
public class MenuButtonFloors : MonoBehaviour
{
    public MenuButtonsUpAndDown MenuButtonsUpAndDown;
    public GameObject ButtonPrefab; 
    private TMP_Text _text;
    
    private Boot _boot; 

    private int _rows; // number of rows

    // [Inject]
    // public void Construct(Boot boot)
    // {
    //     _boot = boot;
    // }
    
    
    void Start() 
    {
        _boot = FindObjectOfType<Boot>().GetComponent<Boot>();
        MenuButtonsUpAndDown = GameObject.FindObjectOfType<MenuButtonsUpAndDown>();
        ButtonsInitialize();
    }

    
    void Update()
    {

    }

    private void ButtonsInitialize() 
    {
        float buttonStartPositionX = Screen.width - 180f;
        float buttonStartPositionY = Screen.height - 50f;
        int buttonsInRow = 0; 
        int maxButtonsInRow = 3; 

        if (_boot != null) 
        {
            for (int floorButton = 0; floorButton < _boot.NumberOfFloors; floorButton++) // 0 to _boot.GetNumberOfFloors()
            {
                GameObject buttonObject = Instantiate(ButtonPrefab, new Vector3(buttonStartPositionX, buttonStartPositionY), Quaternion.identity, transform);
                buttonObject.name = $"Floor {floorButton + 1}"; // set name of button
                buttonsInRow++;
                TMP_Text textMeshPro = buttonObject.GetComponentInChildren<TMP_Text>(); 
                if (textMeshPro!= null){ textMeshPro.text = $"{floorButton + 1}"; } // set floor number on button

                Button buttonComponent = buttonObject.GetComponent<Button>(); // get button component
                if (buttonComponent != null) 
                {
                    int floorNumber = floorButton + 1; 
                    buttonComponent.onClick.AddListener(() => MenuButtonsUpAndDown.MoveToTargetFloor(floorNumber)); // add listener to button
                }
                

                if (buttonsInRow > maxButtonsInRow)
                {
                    buttonStartPositionX = Screen.width - 180f;  // reset x position
                    buttonStartPositionY -= 70f; // move y position
                    buttonsInRow = 1;           // reset number of buttons in row
                }

                else
                {
                    buttonStartPositionX += 50f; // move x position
                }
            }
        }
        else
        {
            Debug.LogError("_boot is null");
        }
    }
}
}
