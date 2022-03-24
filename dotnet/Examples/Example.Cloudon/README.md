<h1 align="center">
  CloudOn Example
</h1>

## 🚀 Quick start

1.  **Prepare Applications on Visual Studio**

    1. Use Visual Studio to to open the solution file `Example.Cloudon.sln` .
    1. On Visual Studio right click Solution `Example.Cloudon` folder and select properties 
    1. Select `Common Properties` > `Startup Project` > `Multiple startup projects`
        1. Project `Example.Cloudon.API` -> Action: `Start`
        1. Project `Example.Cloudon.Web` -> Action: `Start`
    1. Select `Apply` and `OK`

    `

1.  **Prepare Sql Database**

    Use the Package Manager Console ([install instructions](https://www.gatsbyjs.com/docs/tutorial/part-0/#gatsby-cli)) to make database connection and push entities to database.

    ```shell
    add-migration db
    update-database
    ```

1.  **Start running!**
    
    On Visual Studio, right to `Start` button you will see `<Multiple Startup Projects>`. 
    
    If NO, please repeat step 1. If YES, press `Start`. 

    * **Site** is running at [localhost:44369/swagger](https://localhost:44369/swagger)
    * **API UI** (Swagger UI) running at [localhost:44369/swagger](https://localhost:44369/swagger)

## Features
1. API-based solution
1. Swagger UI for the API
1. JWT for API Authorization
1. User password stored hashed

## 🧐 What's inside `Example.Cloudon.API`

A quick look at the infrastructure of the API

    .
    ├── Controllers
    ├── Databases
    ├── Dtos
    ├── Entities
    ├── Helpers
    ├──── Extentions
    ├──── Consts.cs
    ├── Repository
    ├── Services
    ├── DependencyInjectionRegistry.cs

1.  **`/Controllers`**: This directory contains the controllers of the API endpoints

1.  **`/Databases`**: This directory contain all the Database connections

1.  **`/Dtos`**: This directory contain all Data Transfer Object classes

1.  **`/Entities`**: This directory contain all Entities of the main database (`Databases/ApplicationDbContext.cs`)

1.  **`/Helpers`**: This directory contain Helpers for everything

1.  **`/Extentions`**: This directory contain Extentions for everything

1.  **`/Consts.cs`**: This file is a directory of keys and configurations

1.  **`/Repository`**: This directory contain Interfaces ussing for CRUD opperations. **Important:** this is the only directory that is connected with the databases!

1.  **`/Services`**: This directory contain Interfaces ussing for the most important opperations except of CRUD opperations.

1.  **`/DependencyInjectionRegistry.cs`**: This file used to Register the Interfaces, mostly from `/Repository` and `/Services` directories