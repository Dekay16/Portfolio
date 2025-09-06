Migrations: 
Add-Migration *Migration Name* -StartupProject Portfolio -Project Portfolio.Context
Update-Database -StartupProject Portfolio -Project Portfolio.Context

To Do:
Resume Download button
Admin Features - Activity Tracker, Error Logging, Roles and users
Bootstrap
Chart.js
Datatables.js
Cards with projects displayed
Layout and navigation.
Theme.

Design layout in figma or bootstrap studio if free.

Activity Tracker and error logger with chart.js and datatables. Expandable rows, heat graphs
CRUD Projects. Be able to manually add new project cards

--------------------------------------------------------------------------------------------------------
For new project setup:
Open PMC: Tools → NuGet Package Manager → Package Manager Console
Select the Default Project as your Context/Data project (where ApplicationDbContext is).
Run:
Add-Migration InitialTrafficLog

This generates a migration file that will create the TrafficLog table.

-=-

Run:
Update-Database

This applies the migration and creates the TrafficLog table in your configured database.