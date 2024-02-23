using System.Collections;
using System.Collections.Generic;
using Elevator;
using UnityEngine;
using UnityEngine.UI;

public class updownButtons : MonoBehaviour
{
    public Button buttonUp;
    public Button buttonDown;

    public float moveDuration = 1.0f;
    private bool isMoving = false;
    private int _currentFloor = 1;
    private Boot _boot;


    private GameObject _stage;
    private Building.Elevator.Elevator _elevator;
    void Start()
    {
        _boot = GameObject.Find("Start").GetComponent<Boot>();
        GameObject stage = GameObject.Find("STAGE");
        _stage = stage;
        buttonUp.onClick.AddListener(MoveStageUp);
        buttonDown.onClick.AddListener(MoveStageDown);
    }
    
    public void MoveStageUp()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveStageSmoothly(0));
        }
    }
    public void MoveStageDown()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveStageSmoothly(1));
        }
    }

    IEnumerator MoveStageSmoothly(int direction)
    {
        isMoving = true;
        Vector3 initialPosition = _stage.transform.position;
        Vector3 targetPosition;
        if (direction == 0 && _currentFloor < _boot.GetNumberOfFloors())
        {
            targetPosition = initialPosition + new Vector3(0f, -6f, 0f);
            _currentFloor += 1;
        }
        else if(direction == 1 && _currentFloor > 1)
        {
            targetPosition = initialPosition + new Vector3(0f, 6f, 0f);
            _currentFloor -= 1;
        }
        else
        {
            targetPosition = initialPosition;
            Debug.Log($"HA HA {_boot.GetNumberOfFloors()}");
        }
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            _stage.transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _stage.transform.position = targetPosition;
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
