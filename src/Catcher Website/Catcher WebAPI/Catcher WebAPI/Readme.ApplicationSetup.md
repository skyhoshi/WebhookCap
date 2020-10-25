# Webhook Catcher Site

This project represents an example of a website that catches a webhook's request and processes the hook's result into a datatable

## Features

1. API to Catch Webhook sending xml
1. Middleware to log each request so if parsing or auth should fail a record can be kept to assess security risks or common mistakes.
1. Advanced Custom Identity Control using ASP.NET Core Identity coupled with Entity Framework Core
1. Database Context Interceptors to troubleshoot what's being sent and recieved from your database.
1. Area: API Tester to allow you to test your catcher without having to cause execution from a third party. (Self Localized Testing)


| Feature Name | Location | Description |
|-------------:|----------|-------------|
|Webhook API Catch|||
|Webhook Log Middleware|||
|Identity Control|||
|db~Interceptors|~/Data/Diagnostics/Interceptors|these were used to deal with a trigger issue, what was happening is a trigger would go off and depending on a SQL error the data would be removed from the datatable due to auto transactioning on the database|


## Project Setup

### Database
IMPORTANT: There are three seperate database contexts that depend on different connection strings. This allows you to store different contexts in different databases.


#### Development Testing
Run the following within Visual Studio's Package Manager Console
```powershell
Update-Database -Context GoCanvasDbContext -Project CatchWatch;
Update-Database -Context GoCanvasLoggingDbContext -Project CatchWatch;
Update-Database -Context ApplicationDbContext -Project CatchWatch;
```

#### Production Installation
it's suggested that you create TSQL Scripts for a DBA To Review. (if you have a DBA)
Use the following to generate those scripts for running and execution. 

```powershell
Script-DbContext -Context GoCanvasDbContext -Project CatchWatch;
Script-DbContext -Context GoCanvasLoggingDbContext -Project CatchWatch;
Script-DbContext -Context ApplicationDbContext -Project CatchWatch;
```

These three generated Scripts should be gathered and submitted together. (Depending on your companies policies)


