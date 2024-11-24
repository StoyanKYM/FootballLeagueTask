# FootballLeague

This is a quick and simple app that showcases my abilities with .NET Core. The application consists of one page showcasing rankings for the teams in an imaginary Football League.
Every time a new entry is added(match played) the Ranking Table in the Index Page updates accordingly. Matches that are not played can still be added, but won't be showed in the rankings table. This leaves room for future improvements like adding Date for each match and Status column in the Index Page/Ranking Page. Matches that are added as Not Played(IsPlayed:false) cannot be modified.
The application consist endpoints for CRUD operations for both Teams and Matches.

### Business Requirements
Create an app which will be used to display:
- Teams ranking
- Teams played matches

### The app consist logic for
- CRUD operations for teams
- CRUD operations for matches(only played matches can be updated)
- Table for the rankings and results(updated each time a new Match is added)

### Technical Requirements
- Duration: 2 days
- Template: .NET Core(MVC) – in order to have a front page with ranking
- Technologies used: MSSQL, Entity Framework Core, NET Core, Postman, Swagger
- Design Patterns & Principles: SOLID

### Other
Things to Improve: ViewModels, DTOs,Upcoming matches, Status of matches(played/not played), Unit Tests
To test the app: https://localhost:<port>/swagger/index.html
GitHub repo: 

## How to use the Application
Prerequisites: NET Core, MSSQL, SSMS
1. Clone the repo
2. Open the project and In appsettings.json add your own ConnectionString
3. In Package Manager Console execute both commands:
- Add-Migration {name of the migration}
-  Update-Database
- End Result: A DB should be created in your SSMS.
4. Run the Application

### Instructions:
1. Open https://localhost:<port>/swagger/index.html
2. You can test the CRUD operations with the examples shown below or use your own JSON.

### Examples:
Check the Documentation provided with the app.
*Try using Swagger with your own JSON, there may be some endcases where the ranking results are incorrect.
