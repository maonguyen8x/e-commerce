## GUIDLINE
### Install the JwtBearer NuGet Package
PM> Install-Package Microsoft.AspNetCore.Authentication.JwtBearer

## Specify a Secret Key in the appsettings.json file
Add the following information in the appettings.json file.
appsettings.json
 "JwtSettings": {
    "SigningKey": "YouSecreteKeyforAuthenticationtovalidateApplication",
    "Issuer": "AuthBasicAPI",
    "Audiences": [ "Swagger-Client" ]
  },

## Dependencies <a name="backend-dependencies"></a> 
- AutoMapper version 12.0.1
- AutoMapper.Extensions.Microsoft.DependencyInjection version 12.0.1
- BCrypt.Net-Next version 4.0.3
- Microsoft.AspNetCore.Authentication.JwtBearer version 7.0.8

## Backend <a name="backend-technologies"></a> # BACKEND NOT DEPLOYED IN AZURE ANYMORE

- .NET
- Entity Framework 7
- PostgreSQL

## Endpoints <a name="endpoints"></a> 

| Method |       Endpoint            |          Description                 | Authorization |
|--------|---------------------------|--------------------------------------|---------------|
| Post   | api/v1/Auths              | Login and obtain JWT token           | Not required  |
|        |                           |                                      |               |
| Get    | api/v1/Products           | Get a list of all products           | Not required  |
| Get    | api/v1/Products/{id}      | Get a single product by ID           | Not required  |
| Post   | api/v1/Products           | Create a new product                 | Bearer Token  |
| Put    | api/v1/Products/{id}      | Update an existing product by ID     | Bearer Token  |
| Delete | api/v1/Products/{id}      | Delete an employee by ID             | Bearer Token  |
|        |                           |                                      |               |
| Get    | api/v1/Categories         | Get a list of all categories         | Not required  |
| Get    | api/v1/Categories/{id}    | Get a single category by ID          | Not required  |
| Post   | api/v1/Categories         | Create a new category                | Bearer Token  |
| Put    | api/v1/Categories/{id}    | Update an existing category by ID    | Bearer Token  |
| Delete | api/v1/Categories/{id}    | Delete an category by ID             | Bearer Token  |
|        |                           |                                      |               |
| Get    | api/v1/Carts              | Get a list of all carts              | Not required  |
| Get    | api/v1/Carts/{id}         | Get a single cart by ID              | Not required  |
| Post   | api/v1/Carts              | Create a new cart                    | Bearer Token  |
| Put    | api/v1/Carts/{id}         | Update an existing cart by ID        | Bearer Token  |
| Delete | api/v1/Carts/{id}         | Delete an cart by ID                 | Bearer Token  |
|        |                           |                                      |               |
| Get    | api/v1/CartItems          | Get a list of all cart items         | Not required  |
| Get    | api/v1/CartItems/{id}     | Get a single cart item by ID         | Not required  |
| Post   | api/v1/CartItems          | Create a new cart item               | Bearer Token  |
| Put    | api/v1/CartItems/{id}     | Update an existing cart item by ID   | Bearer Token  |
| Delete | api/v1/CartItems/{id}     | Delete an cart item by ID            | Bearer Token  |
|        |                           |                                      |               |
| Get    | api/v1/Users              | Get a list of all users              | Not required  |
| Get    | api/v1/Users/{id}         | Get a single user by ID              | Not required  |
| Post   | api/v1/Users              | Create a new user                    | Not required  |
| Put    | api/v1/Users/{id}         | Update an existing user by ID        | Not required  |
| Delete | api/v1/Users/{id}         | Delete an user by ID                 | Not required  |

## Server:
```Bash
PM> dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore --version 8.0.10
PM> dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version 8.0.8
PM> dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 8.0.10
PM> dotnet add package Microsoft.AspNetCore.Identity --version 2.2.0
PM> dotnet add package BCrypt.Net-Next --version 4.0.3
PM> dotnet add package PM> Microsoft.IdentityModel.Tokens --version 8.1.2