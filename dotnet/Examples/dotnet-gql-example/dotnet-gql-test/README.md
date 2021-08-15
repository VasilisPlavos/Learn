Source: https://dev.to/mnsr/entity-framework-dotnet-core-with-graphql-and-sql-server-using-hotchocolate-4fl6

# Technologies
* ASP.Net Core
* EF
* Hotchocolate
* Graphql-dotnet

# How to run
1. add-migration UDb1
2. update-database
3. Add some data in database
4. Start app
5. Go to https://localhost:44302/playground
6. Try queries

### Query examples
```
{
  locations {
    name
    code
  }
}
```

```
{
  location(code: "lax") {
    name
  }
}
```