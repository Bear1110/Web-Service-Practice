# Web-Service-Practice
Server use .NET Core.

## Server .Net Core API

Reference Link [MSDN](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api)

## SignalR

[Reference](https://docs.microsoft.com/zh-tw/aspnet/core/tutorials/signalr)

use `/chathub` as connect channel.

[Signal By Group](https://docs.microsoft.com/en-us/aspnet/core/signalr/groups?view=aspnetcore-3.1)

## deploy to iis 

From visual studio -> build option

But install the  `.NET Core Windows Server Hosting` is necessary. (.net 3.1)

And then setting the permission.

[Reference](https://blog.johnwu.cc/article/iis-run-asp-net-core.html) [Download](https://dotnet.microsoft.com/download/archives)

ActionFilterAttribute can do something before,after,beforeResult,afterResult action 


## SQL Server 2019 Express

> LocalDB 為輕量版的 SQL Server Express 資料庫引擎，鎖定程式開發為其目標。 LocalDB 會依需求啟動，並以使用者模式執行，因此沒有複雜的組態。
> C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA

In `Package Manager Console` use this `update-database -verbose` to create and update table.


[EntityState.Modified; Only store the value what your object Have.](https://stackoverflow.com/questions/30252118/ef-update-using-entitystate-modified )


### Lazy Loading and data linking
[lazy loading install Microsoft.EntityFrameworkCore.Proxies](https://docs.microsoft.com/en-us/ef/core/querying/related-data/lazy)

Otherwise use this way to let it Contrete => [Eager loading](https://docs.microsoft.com/en-us/ef/core/querying/related-data/eager)
```
await _context.Rooms.Include(m=>m.Player1).ToListAsync();
```

[Releation](https://docs.microsoft.com/en-us/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key)
Refetence [link1](https://docs.microsoft.com/zh-tw/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/creating-a-more-complex-data-model-for-an-asp-net-mvc-application) [link2](https://www.entityframeworktutorial.net/code-first/configure-one-to-one-relationship-in-code-first.aspx)

Dapper other way to do the data link(Foreign key)

### Reference

很多參考這個
https://blog.poychang.net/asp-net-core-webapi-with-entity-framework-core/


query by dbset https://www.learnentityframeworkcore.com/dbset/querying-data


Micosoft Example for db [link1](https://www.microsoft.com/en-us/sql-server/developer-get-started/csharp/win/step/2.html) [link2](https://github.com/microsoft/sql-server-samples/blob/master/samples/tutorials/c%23/Windows/SqlServerSample/Program.cs)

#### EnsureCreated 
如果你有兩個Table A B
這個function 只有在A跟B都不再的時候才會創
有A 不會創 B
EnsureCreated will create the database if it doesn't exist and initialize the database schema.

If any tables exist (including tables for another DbContext class), the schema won't be initialized.