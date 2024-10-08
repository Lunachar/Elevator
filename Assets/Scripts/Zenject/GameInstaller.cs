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
        [SerializeField] private PersonGO personGo;
        [SerializeField] private MainMenu mainMenu;
        [SerializeField] private GameManager gameManager;
        
        public override void InstallBindings()
        {
            Container.BindInstance(bootInstance).AsSingle();
            Container.BindInstance(unityDisplayInstance).AsSingle();
            Container.BindInstance(mainMenu).AsSingle();
            //Container.BindInstance(gameManager).AsSingle();
            Container.Bind<GameManager>().FromComponentInNewPrefab(gameManager).AsSingle().NonLazy();
            
            
            // Container.Bind<MainMenu>().AsSingle();
            Container.Bind<Building>().AsSingle();
            Container.Bind<FloorFactory>().AsSingle();
            Container.Bind<Floor>().AsTransient();
            Container.Bind<FloorList>().AsSingle();
            Container.Bind<Person>().AsTransient();
            Container.Bind<PersonGenerator>().AsSingle();
            Container.Bind<ConsoleDisplay>().AsSingle();
            //Container.Bind<UnityDisplay>().AsSingle();
            Debug.Log("in GameInstaller");
            Container.Bind<IElevator>()
                .To<Elevator>()
                .AsSingle();
            Container.Bind<DatabaseManager>().AsSingle();
            Container.Bind<DB_Setup>().AsSingle();
        }

        
    }
        
}