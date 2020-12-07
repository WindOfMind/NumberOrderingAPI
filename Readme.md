# Number ordering API

Web API that provides 2 endpoints:

1. We can pass line of numbers and numbers be ordered and saved to file: `POST /api/numbers`.
2. We can load content of latest saved file: `GET /api/numbers`.

Solution contains next projects:
- *NumberOrdering.API* project that contains ASP.NET Core Web API
- *NumberOrdering.Domain* project that contains sorting and saving functionality for numbers
- *NumberOrdering.Infrastructure* project that contains capabilities for saving and reading numbers to/from files
- *NumberOrdering.Tests* project that contains unit tests for crucial logic of the solution

### Sorting algorithm

This solution uses *QuickSort* algorithm for sorting numbers as it is a commonly used algorithm for sorting and it works relatively fast and requires constant additional space. 
We can also consider *count sorting* if we always know the limits of numbers (e.g. numbers can't exceed 100) from a context.

## How to run

Build solution.
There are several options to run application:
- IIS Express
- Console app
- Docker

Default launch url is a Swagger page `/api/documentation`.

## How to run unit tests

NumberOrdering.Tests project uses XUnit framework for unit testing.
Use your favorite test runner (e.g. MSTest, R#) for executing all unit tests in this project.