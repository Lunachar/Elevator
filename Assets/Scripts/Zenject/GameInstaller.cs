using Elevator.Display;
using Elevator.Interfaces;
using Elevator.Managers;
using UnityEngine;
using Zenject;

namespace Elevator
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Debug.Log("1: in GameInstaller");
           
            Container.BindFactory<int, int, int, Floor, FloorFactory>();
            Container.Bind<PersonGenerator>().AsSingle();
            

           
            Container.Bind<ConsoleDisplay>().AsSingle();
            Container.Bind<IElevator>()
                .To<Elevator>()
                .AsSingle();
            
            Container.Bind<Boot>().AsSingle();
            Container.Bind<DatabaseManager>().AsSingle();
            Container.Bind<DB_Setup>().AsSingle();
            Container.Bind<Floor>().AsTransient();
            Container.Bind<Building>().AsSingle();
            //Container.Bind<Person>().AsTransient();
        }

    }
        
}