# Elevator Simulation Project

This is an educational project developed in C# for Unity, focusing on simulating an elevator system. 
The project leverages Zenject for dependency injection, integrates NuGet packages
for generating random user data (such as names and surnames), and implements SQLite for data storage. 

## Features

- **Dependency Management with Zenject:** Manages dependencies efficiently within the Unity environment.
- **Random Data Generation:** Utilizes NuGet packages to create people with random data (including name, surname, etc.).
- **SQLite Database Integration:** Stores generated data into a SQLite database for persistent storage.
- **Observer Pattern Implementation:** Utilizes the observer pattern to track the elevator's operation.
- **Building with Elevator Creation:** Allows the user to specify the number of floors, elevator capacity, and the number of people.
- **Manual Mode:** Users can manually distribute individuals across floors, tracking the number of steps taken.
- **Automated Mode:** The program repeats the manual task using its algorithms.
- **Comparison of Manual and Automated Modes:** Compares the results obtained in manual and automated modes.

## Usage

1. **Setup the Environment:** Ensure Unity and necessary NuGet packages are installed.
2. **Run the Simulation:** Launch the simulation, specify floor count, elevator capacity, and the number of people.
3. **Manual Mode:** Distribute individuals across floors manually and record the steps taken.
4. **Automated Mode:** Let the program distribute individuals using its algorithms and compare the results.
5. **Analysis:** Compare the efficiency between manual and automated distributions.

## Contribution

Contributions, suggestions, and improvements are welcome. Feel free to create issues or pull requests.
