using Elevator.Display;
using Elevator.Interfaces;
using UnityEngine;
using Zenject;

namespace Elevator
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Debug.Log("1: in GameInstaller");
            Container.Bind<Boot>().AsSingle();
            Container.BindFactory<int, int, int, Floor, FloorFactory>();
            Container.Bind<DB_Setup>().AsSingle();
            Container.Bind<Floor>().AsSingle();
           
            Container.Bind<ConsoleDisplay>().AsSingle();
            Container.Bind<IElevator>()
                .To<Elevator>()
                .AsSingle();
            
            //Container.Bind<Building>().AsSingle();
            //Container.Bind<Person>().AsSingle();
        }

    }
        
}