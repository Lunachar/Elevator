using System.Collections.Generic;
using Elevator.Interfaces;

namespace Elevator.Managers
{
    public class DatabaseManager
    {
        private DB_Setup _dbSetup;

        public DatabaseManager(DB_Setup dbSetup)
        {
            _dbSetup = dbSetup;
        }

        public void SavePeopleToDB(List<Person> personsList)
        {
            foreach (var person in personsList)
            {
                _dbSetup.InsertPersonData(person);
            }
        }

    }
}