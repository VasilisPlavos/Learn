# Auth with JWT & custom middlewares

The idea here is that we can have middlewares for everything. Check CustomMiddlewaresRegistry.cs, CustomMiddleware.cs and Program.cs

### How to run:
* add-migration db1
* update-database
* start application

### Information
This is an API that users can create and own random projects.

In this API both Anonymous or Authorized users can be authenticated and have their own projects.

A user can also claim the ownership of a project, using the JWT of the other user.