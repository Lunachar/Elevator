using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Elevator
{
    public class PrefabInstaller : MonoInstaller
    {
        public GameObject buildingGo;
        public Transform buildingSpawnPosition;
        
        public GameObject elevatorGo;
        public GameObject floorGo;
        public GameObject personGo;

        public override void InstallBindings()
        {
            BindBuildingGO();
            BindElevatorGO();
            BindFloorGO();
            BindPersonGO();
        }
        
        private void BindBuildingGO()
        {
            // var buildingInstance = Container
            //     .InstantiatePrefabForComponent<BuildingGO>(buildingGo, buildingSpawnPosition.position, Quaternion.identity,
            //         null);
            
            Container
                .Bind<GameObject>()
                .FromInstance(buildingGo)
                .AsSingle();
        }
        
        private void BindElevatorGO()
        {
            Container
                .Bind<GameObject>()
                .FromInstance(elevatorGo)
                .AsSingle();
        }
        private void BindFloorGO()
        {
            Container
                .Bind<GameObject>()
                .FromInstance(floorGo)
                .AsSingle();
        }
        private void BindPersonGO()
        {
            Container
                .Bind<GameObject>()
                .FromInstance(personGo)
                .AsSingle();
        }
        
        
    }
}
