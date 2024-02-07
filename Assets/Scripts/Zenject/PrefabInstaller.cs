using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Elevator
{
    public class PrefabInstaller : MonoInstaller
    {
        public BuildingGO buildingGo;
        public ElevatorGO elevatorGo;
        public FloorGO floorGo;
        public PersonGO personGo;

        public EmptyObject emptyObject;

        public override void InstallBindings()
        {
            BindBuildingGO();
            BindElevatorGO();
            BindFloorGO();
            BindPersonGO();
        }
        
        private void BindBuildingGO()
        {
            Container
                .Bind<BuildingGO>()
                .FromInstance(buildingGo)
                .AsSingle();
        }
        
        private void BindElevatorGO()
        {
            Container
                .Bind<ElevatorGO>()
                .FromInstance(elevatorGo)
                .AsSingle();
        }
        private void BindFloorGO()
        {
            Container
                .Bind<FloorGO>()
                .FromInstance(floorGo)
                .AsSingle();
        }
        private void BindPersonGO()
        {
            Container
                .Bind<PersonGO>()
                .FromInstance(personGo)
                .AsSingle();
        }
        private void BindEmptyObject()
        {
            Container
                .Bind<EmptyObject>()
                .FromInstance(emptyObject)
                .AsSingle();
        }
        
    }
}
