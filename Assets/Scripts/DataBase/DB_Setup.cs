using System.IO;
using Elevator;
using Mono.Data.Sqlite;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using Zenject;

public class DB_Setup
{
    internal string currDbName = "Elevator_curr.db";
    internal string prevDbName = "Elevator_prev.db";
    private Person _person;

    // [Inject]
    // public void Construct()
    // {
    //     CreateDB(currDbName)
    // }


    
    public void CreateDB(string dbName)
    {
        using (var connection = new SqliteConnection($"URI=file:{dbName}"))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText =
                    "CREATE TABLE IF NOT EXISTS personData (id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT CHECK ( id >= 0 AND id <= 300 ), " +
                    "name VARCHAR(20), " +
                    "lastName VARCHAR(20), " +
                    "age TINYINT UNSIGNED, " +
                    "currentFloor TINYINT, " +
                    "targetFloor TINYINT, " +
                    "completed BOOL);";

                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public void InsertPersonData(Person person)
    {
        //RotateDatabase();
        using (var connection = new SqliteConnection($"URI=file:{currDbName}"))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = 
                    "INSERT INTO personData (name, lastName, age, currentFloor, targetFloor, completed)" +
                    "VALUES (@name, @lastName, @age, @currentFloor, @targetFloor, @completed);";
                
                command.Parameters.AddWithValue("@name", person.Name);
                command.Parameters.AddWithValue("@lastName", person.LastName);
                command.Parameters.AddWithValue("@age", person.Age);
                command.Parameters.AddWithValue("@currentFloor", person.CurrentFloor);
                command.Parameters.AddWithValue("@targetFloor", person.TargetFloor);
                command.Parameters.AddWithValue("@completed", false);

                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
}
