using System.Collections.Generic;
using System.IO;
using Elevator.Interfaces;
using UnityEngine;

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
        public void RotateDatabase()
        {
            if (File.Exists(_dbSetup.currDbName))
            {
                if (File.Exists(_dbSetup.prevDbName))
                {
                    File.Delete(_dbSetup.prevDbName);
                }
            
                File.Move(_dbSetup.currDbName, _dbSetup.prevDbName);
            }
        
            _dbSetup.CreateDB(_dbSetup.currDbName);
        }

    }
}