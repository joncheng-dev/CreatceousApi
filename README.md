# Cretaceous API

#### By Jonathan Cheng

## Description

A Web API app that shares data about a wildlife park consisting of creatures from the cretaceous period -- complete with full CRUD functionality. Data is accessible by a client. Pagination is used to display a limited number of results at a time.

- _This web API application is written using C#, run using .NET framework, and database query and relationships handled using Entity Framework Core._
- _Data annotations and conditionals are in place to validate user input._
- _Full CRUD functionality for the class / model._
- _Data storage is managed using MySQL. Entity Framework Core .NET Command-line Tools (or dotnet ef) is used for database version control -- migrations are created to tell MySQL how the database is structured and updated as needed._
- _Swashbuckle (Swagger, Swagger UI) and Postman are used to document and test API endpoints._

## Technologies Used

- _C#_
- _.NET 6_
- _ASP.NET Core MVC_
- _MySQL_
- _MySQL Workbench_
- _Entity Framework Core_
- _Swashbuckle v6.2.3_
- _Postman v10.19_

## Setup/Installation Requirements

### Required Technology

- _Verify that you have the required technology installed for [MySQL](https://dev.mysql.com/doc/mysql-installation-excerpt/5.7/en/) and [MySQL Workbench](https://dev.mysql.com/doc/workbench/en/)._
- _Also check that Entity Framework Core's `dotnet-ef` tool is installed on your system so that it can perform database migrations and updates. Run the following command in your terminal:_
  > ```bash
  > $ dotnet tool install --global dotnet-ef --version 6.0.0
  > ```

### Setting Up the Project

#### Clone the Project

_1. Open your terminal (e.g., Terminal or GitBash)._

_2. Navigate to where you want to place the cloned directory._

_3. Clone the repository from the GitHub link by entering in this command:_

> ```bash
> $ git clone https://github.com/joncheng-dev/CretaceousApi.Solution.git
> ```

#### Set up a Connection String to Database

_Navigate to the project's production directory `CretaceousApi.Solution/CretaceousApi`, and create a new file called `appsettings.json`. Within the `appsettings.json` file, add the following code snippet. Change the server and port and needed. Replace the `uid`, and `pwd` values with your username and password for MySQL. Under `database`, add a fitting name -- although `cretaceous_api` is suggested for clarity of purpose._

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;database=[YOUR-DATABASE-NAME-HERE];uid=[YOUR-USERNAME-HERE];pwd=[YOUR-PASSWORD-HERE];"
  }
}
```

#### Check for Port Conflicts

_Make sure your ports are different for this Web API app and your client app -- if you are running a client app as a means to make requests from the API. In this app's `Properties/launchSettings.json` file, check to see that the `applicationUrl` key is set to a different set of ports than your client app. (i.e. in the CretaceousApi app, we have it set to 5001 and 5000 as shown in the `launchSettings.json` snippet below)._

```json
"CretaceousApi": {
    "commandName": "Project",
    "dotnetRunMessages": true,
    "launchBrowser": true,
    "launchUrl": "swagger",
    "applicationUrl": "https://localhost:5001;http://localhost:5000",
    "environmentVariables": {
      "ASPNETCORE_ENVIRONMENT": "Development"
    }
  }
```

#### Generate the Database

_In the terminal, while in the project's production directory `CretaceousApi`, run the command below. It will utilize the repository's migrations to generate the database with appropriate modifications via Entity Framework Core. You may opt to verify that the database has been created successfully in MySQL Workbench._

> ```bash
> $ dotnet ef database update
> ```

## Launching the API

_In the command line, while in the project's production directory `CretaceousApi.Solution/CretaceousApi`, enter the command `dotnet run` to compile and execute the application. Afterwards, the API is accessible via a client._

> ```bash
> $ dotnet run
> ```

## API Documentation

To explore the API endpoints, use a client such as a browser, Postman, or Swagger UI. If using Swagger, navigate to the following URL: `https://localhost:5001/swagger/index.html`, or `http://localhost:5000/swagger/index.html`.

#### Note on Pagination

CretaceousApi is set to return the first index of results, and show up to 10 results per page. To make modifications, opt to do one of the following:

- _Navigate to the project's Controller directory, file `AnimalsController.cs`, and edit the HttpGet controller's Get() action parameters `pageIndex = 1` and `int pageSize = 10` to different integer values for pageIndex and pageSize respectively._
- _Change the parameters in the http request: `http://localhost:5000/api/Animals?pageIndex=1&pageSize=10`. Set the parameters `pageIndex` and `pageSize` to different integer values, such as 2 for pageIndex to see the second page of all results, and 20 for pageSize to return twenty results per page, respectively._

### API Endpoints

Base URL: `http://localhost:5000`

#### HTTP Request Structure

| Request Type |       Path        |
| :----------: | :---------------: |
|     GET      |   /api/animals    |
|     GET      | /api/animals/{id} |
|     POST     |   /api/animals    |
|     PUT      | /api/animals/{id} |
|    DELETE    | /api/animals/{id} |

#### Path Parameters

| Parameter  |  Type   | Default | Required | Description                                             |
| :--------: | :-----: | :-----: | :------: | ------------------------------------------------------- |
| pageIndex  | integer |    1    |  false   | Returns the requested index page.                       |
|  pageSize  | integer |   10    |  false   | Returns up to the requested number of animals per page. |
|  species   | string  |  none   |  false   | Returns matches by species.                             |
|    name    | string  |  none   |  false   | Returns matches by animal name.                         |
| minimumAge | integer |  none   |  false   | Returns animals at least specified age or older.        |

#### Example Queries

The following query will return the first ten results.

```
http://localhost:5000/api/Animals?pageIndex=1&pageSize=10
```

The following query will return the first ten results of animals of species dinosaur.

```
http://localhost:5000/api/Animals?species=dinosaur&pageIndex=1&pageSize=10
```

The following query will return the first ten results of animals of species dinosaur, with minimum age 5.

```
http://localhost:5000/api/Animals?species=dinosaur&minimumAge=20&pageIndex=1&pageSize=10

```

#### Sample JSON Response

```json
{
  "animalId": 5,
  "name": "Bartholomew",
  "species": "Dinosaur",
  "age": 22
}
```

## Known Bugs

- _Currently no known bugs. Kindly notify me if you happen upon one: joncheng.dev@gmail.com_

## License

```
MIT License

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

Copyright (c) 2023 Jonathan Cheng
```

<a align=left href="#">Return to Top</a>
