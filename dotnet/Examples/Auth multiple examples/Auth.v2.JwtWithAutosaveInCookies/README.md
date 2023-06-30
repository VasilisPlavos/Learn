# Auth with JWT & Access Token

This solution is unfinished but there is the concept and the code!

### How to run:
* add-migration db1
* update-database
* start application

### Information
This is an API that users can create and own random projects.

If user has no JWT but request to create a project then we create a new Anonymous user with a JWT and assign the new project here.

In this API both Anonymous or Authorized users can be authenticated and have their own projects.

A user can also claim the ownership of a project, using the JWT of the other user.