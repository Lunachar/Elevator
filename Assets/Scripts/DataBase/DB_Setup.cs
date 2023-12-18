using Elevator;
using Mono.Data.Sqlite;
using Unity.VisualScripting.Dependencies.Sqlite;
using Zenject;

public class DB_Setup
{
    private string dbName = "URI=file:Elevator.db";
    private Person _person;

    [Inject]
    public void Construct()
    {
        
    }
    
    public void CreateDB()
    {
        using (var connection = new SqliteConnection(dbName))
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
        using (var connection = new SqliteConnection("URI=file:" + dbName))
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
            }
        }
    }
}
