using System;
using Elevator.Interfaces;
using UnityEngine;
using Zenject;

namespace Elevator.Display
{
    public class UnityDisplay : MonoBehaviour, IObserver
    {
         private Building _building;
         private FloorList _floorList;

         private GameObject _buildingGO;
         
         private int _numberOfFloors;

         

         [Inject]
         public void Construct(Building building, FloorList floorList)
         {
             _building = building;
             _floorList = floorList;
         }
        
        

        public void Start()
        {
            _numberOfFloors = _building._floorList.GetFloors().Count;

            Debug.Log($"Count: {_numberOfFloors} \n Total Floors: {_numberOfFloors}");
            
            //VisualizeBuilding();
        }

        internal void VisualizeBuilding(GameObject buildingPrefab, GameObject elevatorPrefab, GameObject floorPrefab, GameObject personPrefab)
        {
            int floorHeight = 5;
            for (int i = 1; i < _numberOfFloors; i++)
            {
                GameObject floor = Instantiate(floorPrefab, new Vector3(0f, i * floorHeight, 0f), Quaternion.identity);
                //floor.transform.SetParent(buildingGO.transform);
            }
        }


        // public void SetFloor(Floor floor)
        // {
        //     _floor = floor;
        // }

        
        

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}