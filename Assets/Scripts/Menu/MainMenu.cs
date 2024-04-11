using System;
using System.Collections;
using System.Collections.Generic;
using Elevator;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Slider numberOfFloorsSlider;     // slider to change the number of floors
    public Slider elevatorCapacitySlider;   // slider to change the elevator capacity
    public Slider maxPeoplePerFloorSlider;  // slider to change the max people per floor

    public static MainMenu instance { get; private set; }

    public static int NumberOfFloors;
    public static int ElevatorCapacity;
    public static int MaxPeoplePerFloor;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        NumberOfFloors = (int)numberOfFloorsSlider.value;
        ElevatorCapacity = (int)elevatorCapacitySlider.value;
        MaxPeoplePerFloor = (int)maxPeoplePerFloorSlider.value;

        SceneManager.LoadScene("GameScene");
    }
}