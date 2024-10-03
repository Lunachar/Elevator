// using System;
// using System.Collections;
// using System.Collections.Generic;
// using TMPro;
// using UnityEngine;
// using Zenject;
//
// public class StatisticField : MonoBehaviour
// {
//     [SerializeField] private TMP_Text MovesIn;
//     [SerializeField] private TMP_Text PassengersLeft;
//
//     private GameManager _gameManager;
//
//     [Inject]
//     public void Construct(GameManager gameManager)
//     {
//         _gameManager = gameManager;
//     }
//     private void Start()
//     {
//         //_gameManager = FindObjectOfType<GameManager>();
//         if (_gameManager  == null)
//         {
//             Debug.LogError("no GAme");
//         }
//     }
//
//     private void Update()
//     {
//         MovesIn.text = _gameManager.GetMoveAmount().ToString();
//         PassengersLeft.text = _gameManager.GetPassengers().ToString();
//     }
// }
