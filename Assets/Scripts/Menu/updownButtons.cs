using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class updownButtons : MonoBehaviour
{
    public Button buttonUp;
    public Button buttonDown;

    public float moveDuration = 1.0f;
    private bool isMoving = false;


    private GameObject _stage;
    private Building.Elevator.Elevator _elevator;
    void Start()
    {
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
        if (direction == 0)
        {
            targetPosition = initialPosition + new Vector3(0f, -6f, 0f);
        }
        else
        {
            targetPosition = initialPosition + new Vector3(0f, 6f, 0f);
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
