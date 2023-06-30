# Auth with JWT & Custom policies attributes

The idea here is that we can use policy using [Authorize(Policy = "PolicyName")] and assign this to a custom policy via Program.cs -> check "Program.cs options.AddPolicy("SessionPolicy", policy..."

### How to run:
* add-migration db1
* update-database
* start application

### Information
This is an API that users can create and own random projects.

In this API both Anonymous or Authorized users can be authenticated and have their own projects.

A user can also claim the ownership of a project, using the JWT of the other user.