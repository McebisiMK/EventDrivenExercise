[![Continuous Integration and deployment](https://github.com/McebisiMK/EventDrivenExercise/actions/workflows/pipeline.yml/badge.svg)](https://github.com/McebisiMK/EventDrivenExercise/actions/workflows/pipeline.yml)
# Event Driven Exercise

Api Project (Api entry point)
-
- [Controllers](https://github.com/McebisiMK/EventDrivenExercise/tree/main/EventDrivenExercise.Api/Controllers)
	- [Users Controller](https://github.com/McebisiMK/EventDrivenExercise/blob/main/EventDrivenExercise.Api/Controllers/UsersController.cs)
		- User's endpoints (**Add, Update and Delete**).
		- Audit log subscriber definition.
- [appsettings](https://github.com/McebisiMK/EventDrivenExercise/blob/main/EventDrivenExercise.Api/appsettings.json)
	- Configurations (**Connection string**).
- [Program.cs](https://github.com/McebisiMK/EventDrivenExercise/blob/main/EventDrivenExercise.Api/Program.cs)
	- Creates WebApplicationBuilder and register services (**AutoMapper, DbContext, DI Resolver, Swagger config**).
	
Common Project (Common functionality of the Application)
-
- [DTOs](https://github.com/McebisiMK/EventDrivenExercise/tree/main/EventDrivenExercise.Common/DTOs)
	-  for mapping DB entities.
- [Event Arguments](https://github.com/McebisiMK/EventDrivenExercise/tree/main/EventDrivenExercise.Common/EventArgurments)
	- Event arguments for passing data from Publisher to Subscriber.
- [Exceptions](https://github.com/McebisiMK/EventDrivenExercise/tree/main/EventDrivenExercise.Common/Exceptions)
	- Custom exceptions.

Core Project (Business logic and object mapping)
-
- [Abstractions](https://github.com/McebisiMK/EventDrivenExercise/tree/main/EventDrivenExercise.Core/Abstractions)
	- Service interfaces.
- [Mapping](https://github.com/McebisiMK/EventDrivenExercise/tree/main/EventDrivenExercise.Core/Mapping)
	- Object mapping profiles (From Entities to DTO and reverse).
- [Services](https://github.com/McebisiMK/EventDrivenExercise/tree/main/EventDrivenExercise.Core/Services)
	- [User Service](https://github.com/McebisiMK/EventDrivenExercise/blob/main/EventDrivenExercise.Core/Services/UserService.cs)
		- Business logic for **Adding, Updating and Deleting** users
		- Publishes user events (**Created, Updated and Deleted**)
	- [Audit Log Service](https://github.com/McebisiMK/EventDrivenExercise/blob/main/EventDrivenExercise.Core/Services/UserAuditLogService.cs)
		- Business logic for **Adding** audit log
		- Subscribes to user events (**Created, Updated, Deleted**)

Data Access Project (ORM and tables)
-
- [Abstractions](https://github.com/McebisiMK/EventDrivenExercise/tree/main/EventDrivenExercise.Data/Abstractions)
    -   Service interfaces.
 - [Models](https://github.com/McebisiMK/EventDrivenExercise/tree/main/EventDrivenExercise.Data/Models)
	 - [Entities](https://github.com/McebisiMK/EventDrivenExercise/tree/main/EventDrivenExercise.Data/Models/Entities)
		 - DB Entities (Tables)
	 - [Event Driven DbContext](https://github.com/McebisiMK/EventDrivenExercise/blob/main/EventDrivenExercise.Data/Models/EventDrivenDbContext.cs)
		 - Entity Framework Core DbContext.
 - [Repositories](https://github.com/McebisiMK/EventDrivenExercise/tree/main/EventDrivenExercise.Data/Repositories)
	 - [Generic repository](https://github.com/McebisiMK/EventDrivenExercise/blob/main/EventDrivenExercise.Data/Repositories/Repository.cs) and [Unit of Work](https://github.com/McebisiMK/EventDrivenExercise/blob/main/EventDrivenExercise.Data/Repositories/UnitOfWork.cs)

Tech Use
-
- **[.Net Core 6](https://docs.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-6.0?view=aspnetcore-6.0)**
- **[Entity Framework Core](https://www.entityframeworktutorial.net/efcore/entity-framework-core.aspx)**
- **[AutoMapper](https://docs.automapper.org/en/stable/Getting-started.html)**
- **[Swagger UI](https://swagger.io/tools/swagger-ui/)**
- **[NUnit](https://nunit.org/)**
- **[NSubstitute](https://nsubstitute.github.io/help/getting-started/)**
- **[Fluent Assertions](https://fluentassertions.com/introduction)**