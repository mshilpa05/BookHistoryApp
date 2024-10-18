# BookHistoryApp

Book history app is an application you can use to manage book entity and view its history of changes.

When you run the solution, you can see the swagger page. You can do CURD operations on Book entity. The updates you do to a book entity is tracked. You change history logs are:
* paginated
* filtered w.r.t publish year
* ordered w.r.t date of the record
* count of actions on each book entity (grouping)

## Technologies used

* ASP.NET Core Web api
* Entity Framework Core 
* SQLLite

### Patterns used
* Clean architecture
