using System.Collections.Generic;
using Elevator.Display;
using Elevator.Interfaces;
using Elevator.Managers;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Elevator
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Boot bootInstance;
        [SerializeField] private UnityDisplay unityDisplayInstance;
        public override void InstallBindings()
        {
            Container.BindInstance(bootInstance).AsSingle();
            Container.BindInstance(unityDisplayInstance).AsSingle();
            Debug.Log("1: in GameInstaller");
            Container.Bind<Building>().AsSingle();
            Debug.Log("2: in GameInstaller");
            Container.Bind<FloorFactory>().AsSingle();
            Debug.Log("3: in GameInstaller");
            Container.Bind<Floor>().AsTransient();
            Container.Bind<FloorList>().AsSingle();
            Debug.Log("4: in GameInstaller");

            Container.Bind<Person>().AsTransient();
            Container.Bind<PersonGenerator>().AsSingle();
            Debug.Log("5: in GameInstaller");
            
            Container.Bind<ConsoleDisplay>().AsSingle();
            Debug.Log("6: in GameInstaller");
            //Container.Bind<UnityDisplay>().AsSingle();
            Debug.Log("7: in GameInstaller");
            Container.Bind<IElevator>()
                .To<global::Building.Elevator.Elevator>()
                .AsSingle();
            Container.Bind<DatabaseManager>().AsSingle();
            Debug.Log("8: in GameInstaller");
            Container.Bind<DB_Setup>().AsSingle();
            
            
            
            Debug.Log($"in GameInstaller 2");
            Debug.Log($"in GameInstaller 3");
        }

        
    }
        
}