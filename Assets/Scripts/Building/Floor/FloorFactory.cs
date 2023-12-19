using Zenject;

namespace Elevator
{
    public class FloorFactory : PlaceholderFactory<int, int, int, Floor>
    {
        private readonly DiContainer _container;

        public FloorFactory(DiContainer container)
        {
            _container = container;
        }

        public override Floor Create(int floorNumber, int totalFloors, int maxPeoplePerFloor)
        {
            var dbSetup = _container.Resolve<DB_Setup>();
            return new Floor(floorNumber, totalFloors, maxPeoplePerFloor, dbSetup);
        }

    }
}