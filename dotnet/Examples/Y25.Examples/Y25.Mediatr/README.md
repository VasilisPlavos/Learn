# MediatR Example

This is a simple example of how to use MediatR in a .NET 8 application.

## Overview

The application has two main features:

*   **Ping:** A request/response message that returns "Pong".
*   **Notify:** A fire-and-forget message that does nothing.

## How to run

1.  Clone the repository.
2.  Run `dotnet build` to build the solution.
3.  Run `dotnet test` to run the tests.
4.  Run `dotnet run --project src/MediatRExample.API/MediatRExample.API.csproj` to run the API.

## How to test

You can test the API using a tool like Postman or curl.

*   **Ping:** `GET http://localhost:5000/ping`
*   **Notify:** `POST http://localhost:5000/ping`
