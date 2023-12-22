using System.Collections.Generic;
using System.IO;
using Elevator.Interfaces;
using UnityEngine;

namespace Elevator.Managers
{
    public class DatabaseManager
    {
        private DB_Setup _dbSetup;
        private string fullPath; 

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
            fullPath = Path.GetFullPath(_dbSetup.currDbName);
            Debug.Log($"!!! in Rotate \n {fullPath} \n {File.Exists(_dbSetup.currDbName)}");
            if (File.Exists(_dbSetup.currDbName))
            {
                Debug.Log($"!!! CURR EXIST");
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