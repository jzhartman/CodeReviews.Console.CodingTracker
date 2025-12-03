# Coding Tracker


## Overview
This is a basic console app meant for creating a log of coding sessions performed by a user. It is build on the requirements outlined in The C# Academy's Coding Tracker project (https://www.thecsharpacademy.com/project/13/coding-tracker).
This project was very similar to the previous project within the C# Academy roadmap, the Habit Logger. Due to the simmilarity, I attempted to use this as an opportunity to work with some design patterns. Admittedly, this caused the application to become much more complicated than was neccessary. However, it proved to be a very usefuly learning opportunity.


## Requirements:

Base Requirements
* This is an application where you’ll log your daily coding time.
* Users need to be able to input the date and time of the coding session.
* The application should store and retrieve data from a real database.
* When the application starts, it should create a sqlite database, if one isn’t present.
* It should also create a table in the database, where the codingn sessions will be logged.
* The users should be able to insert, delete, update and view their logged sessions.
* You should handle all possible errors so that the application never crashes.
* Your project needs to contain a Read Me file where you'll explain how your app works. Here's a nice example:
* To show the data on the console, you should use the "Spectre.Console" library.
* You're required to have separate classes in different files (ex. UserInput.cs, Validation.cs, CodingController.cs)
* You should tell the user the specific format you want the date and time to be logged and not allow any other format.
* You'll need to create a configuration file that you'll contain your database path and connection strings.
* You'll need to create a "CodingSession" class in a separate file. It will contain the properties of your coding session: Id, StartTime, EndTime, Duration
* The user shouldn't input the duration of the session. It should be calculated based on the Start and End times, in a separate "CalculateDuration" method.
* The user should be able to input the start and end times manually.
* You need to use Dapper ORM for the data access instead of ADO.NET. (This requirement was included in Feb/2024)
* When reading from the database, you can't use an anonymous object, you have to read your table into a List of Coding Sessions.
* Follow the DRY Principle, and avoid code repetition.

Challenge Requirements

* Add the possibility of tracking the coding time via a stopwatch so the user can track the session as it happens.
* Let the users filter their coding records per period (weeks, days, years) and/or order ascending or descending.
* Create reports where the users can see their total and average coding session per period.
* Create the ability to set coding goals and show how far the users are from reaching their goal, along with how many hours a day they would have to code to reach their goal. You can do it via SQL queries or with C#.


## Technologies

C#
SQLite Database Conenction
Dapper ORM
Spectre.Console


## Operation

### Main Menu
The application opens into the main menu. Behind the scenes, it checks for the existence of a SQLite DB. If none exists, it will create one and seed it with some basic starter data.
Once initialized, the user can select between the menu options:

Track Session --> Allows the user to insert new records into the DB
View/Manage Entries --> Allows the user to view records in the DB and either update or delete them
View Reports --> Provides some basic analytics for the user to see their progress over a specified time period
Manage Goals --> Evaluate, view, create, and delete goals
Exit --> Closes application

### Track Session
This is the portion where the user can insert records into the database. There is a submenu that allows to select an input method.

The option "Enter Start and End Times" will prompt the user for a start time, then and end time and calculate the duration. It will then print the data to the user and ask for confirmation before adding it to the database.

The option "Start Time" will take a timestamp of the current time. The display will now wait for the user to press any key in order to stop the timer. Once the timer is stopped, it uses the start time and stop time to calculate a duration. This is followed by a confirmation prior to inserting into the DB.

### View/Manage Entries
This option first requires the user to select a time period. Once selected, a list of coding sessions is printed. The user can then update or delete individual records within the list.

#### Selecting the Time Period
The options for time period are abbreviated below:

* All --> Retrieves all coding sessions in the database.
* Past Year --> Retrieves all coding sessions from now until (now - 12 months) (day exclusive).
* YTD --> Retrieves all coding sessions from the current calendar year (based on current date/time).
* Custom Week/Month/Year --> Retrieves all coding sessions with a the specified period. User is prompted for the start date and then the end date is calculated based on the period selected.

All retrieved data is ordered by the start time in ascending format (built into the SQL query).

#### Printing the Records
Once the time period is selected, the list of coding sessions is printed to the screen with an index. The user is then given an additional menu to update or delete any record in that list.

#### Updating a Record
A record is selected to update based on the printed index. The user will be asked to enter a new start time. They can either enter a time or submit a blank entry to keep the current start time.
They are then asked to enter an end time. They can either enter a time or submit a blank entry to keep the current end time.
Once both times are provided, a confirmation will appear with the new start time, end time, and duration. Confirming will update the record in the database.

#### Deleting a Record
A record is selected to delete based on the printed index. Once a record is selected, a confirmation will appear with the data for the record. Confirming will delete the record from the database.

### View Reports
Reports give the user the ability to view the total session count, the total time spent coding, and the average time spent coding for each day in the period. This section begins by prompting the user for a time period. It then prints the seesions from that period, followed by the report data.

#### Selecting the Time Period
The options for time period are abbreviated below:

* All --> Retrieves all coding sessions in the database.
* Past Year --> Retrieves all coding sessions from now until (now - 12 months) (day exclusive).
* YTD --> Retrieves all coding sessions from the current calendar year (based on current date/time).
* Custom Week/Month/Year --> Retrieves all coding sessions with a the specified period. User is prompted for the start date and then the end date is calculated based on the period selected.

All retrieved data is ordered by the start time in ascending format (built into the SQL query).

#### Printing the Data
Once the time period is selected, the list of coding sessions is printed to the screen with an index. This is followed by the report data.

### Manage Goals
Goals are a way for the user to set targets for their coding sessions. There are three types of goals: Total Time, Average Daily Time, and Days Per Period. Upon selecting this option, all active goals are evaluated. Any goals that are completed or failed are individually reported to the user.
Once any/all newly completed or failed goals are reported, a list of these goals and all active goals will print. From here the user can then choose to Add a Goal, Delete a Goal, or View Completed goals. Note that navigating away from this screen and returning will remove the newly completed/failed goals from the list.

#### Add Goals
User selects the start time, end time, goal type (Total Time, Average Time, or Days Per Period), and then the Targe Value. Data is validated and then added to the goals table in the DB.

Note: Goals cannot have an end time that has already expired. It MUST be a future time. The logic is that there is no real accomplishment in creating a goal for events that have already occurred.

#### Delete Goal
Allows the user to select and deleted a goal based on the list. The list printed here is all In Progress, Completed, and Failed goals.

#### View Complete Goals
This prints a list of all Completed goals so the user can see their shining accomplishments (Hooray!)


## Challenges

My goal was to attempt to work with some design patterns for the creation of this application. Based on some of the reading in the requirements, I decided to use a version of MVC.
This utilized a set of models for storing and moving data, a view for printing data, and a controller for navigating between these. I also ended up adding in a service layer. The overall structure I went with is outlined below:

<img width="706" height="904" alt="image" src="https://github.com/user-attachments/assets/c8c04394-b3d1-4c92-be7b-67b2aeba471b" />

For the data access, I created a Sqlite Connection Factory. This was injected into a repository for Coding Sessions and one for Goals. This allowed me to inject my connection string in a single place, and use that connection for all CRUD operations.

My coding session and goals repositories inherited from a Generic Repository that contained generic methods for Dapper query and execute commands. (Note that the overloaded methods were needed to fullfill a project requirement that disallowed the use of anonymous objects).

I utilized custom type handlers for Dapper in order to more easily work with DateTime values and my own enums.

For validation, I created my own generic validation results class (this was inspired by how Spectre.Console validated input). This was a really neat way to handle validation and reporting between layers.

UI was done using Spectre.Console. Given additional time, I would love to clean it up and make it more showy, but see that as less important in my learning journey.

## Future Enhancements

* UI should be redesigned to be more user friendly

* The separation of the "View/Manage Entries" and "View Reports" sections has a lot of redundencies. They should be combined in the future.
  
* There is no protection in the program for a corrupted DB file. This will need to be fixed.

