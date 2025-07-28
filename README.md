# ExceptionHandling with ProblemDetails (.NET 8 Web API)

A sample ASP.NET Core 8 Web API project demonstrating **global exception handling** using `ProblemDetails` (RFC 7807 standard). This project shows how to catch all unhandled exceptions and return standardized error responses across your API.

---

## Technologies Used

- ASP.NET Core 8
- C#
- Swagger / Swashbuckle
- ProblemDetails (RFC 7807)
- Custom Middleware
- Git & GitHub

---

## Project Structure

ExceptionHandlingProblemDetails/  
│  
├── Controllers/  
│ └── ProductsController.cs  
│  
├── Middlewares/  
│ └── ExceptionHandlingMiddleware.cs  
│  
├── Models/  
│ └── Product.cs  
│  
├── Program.cs  
└── README.md  


---

## What is ProblemDetails?

`ProblemDetails` is a standardized error response format used in HTTP APIs. It makes error handling predictable and structured.

### Sample error response

```
{
  "type": "https://httpstatuses.com/400",
  "title": "Bad Request",
  "status": 400,
  "detail": "Product ID must be greater than 0.",
  "instance": "/api/products/0"
}
```
----

## How to Run the Project
## Step 1: Clone this repository

```csharp
git clone https://github.com/YOUR_USERNAME/ExceptionHandlingProblemDetails.git
cd ExceptionHandlingProblemDetails
```

## Step 2: Run the project

```
dotnet run
```

## Step 3: Open Swagger in the browser

```
https://localhost:7044/swagger
```

## API Endpoints to Try

| Method | Endpoint          | Description                    |
| ------ | ----------------- | ------------------------------ |
| GET    | /api/products/1   | Returns sample product         |
| GET    | /api/products/0   | Returns 400 BadRequest error   |
| GET    | /api/products/999 | Returns 404 NotFound error     |
| POST   | /api/products     | Returns 500 for negative price |

## Why Use This?

- onsistent error format
- Easy for frontend/API consumers to parse
- Works cleanly with Swagger & Postman
- Avoids exposing internal exception details
