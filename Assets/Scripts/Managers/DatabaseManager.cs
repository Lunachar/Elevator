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

        /// <summary>
        /// Saves a list of people to the database.
        /// </summary>
        /// <param name="personsList">The list of people to save.</param>
        public void SavePeopleToDB(List<Person> personsList)
        {
            foreach (var person in personsList)
            {
                _dbSetup.InsertPersonData(person);
            }
        }

        /// <summary>
        /// Rotates the database by moving the current database file to a backup file.
        /// </summary>
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