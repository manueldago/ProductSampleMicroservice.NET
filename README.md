# Project: ProductRegistrationSystemAPI


## Technologies

- **.NET 8**
- **MediatR**
- **SQLite**
- **Entity Framework Core**

## Project Scaffolding

- ## businessLogic

- ## controllers

- ## data
    - ## cache
    - ## context
    - ## entities
    - ## mediator
    - ## repositories
    - ## sqlliteDB

- ## models

- ## services

- ## sharedKernel
    

## Architecture Overview

### Layered Architecture: A Robust Foundation

The project embraces a layered architecture, a design approach that organizes the application into distinct layers, each serving a specific purpose. This architectural style fosters maintainability, scalability, and the separation of concerns.

1. **Data Repository: Managing the Data Lifecycle**
   
   The foundation of the application lies in the Data Repository layer. This layer is meticulously crafted to handle the storage and retrieval of data, ensuring a seamless interaction with the underlying database, which, in this case, is SQLite. The Data Repository layer encapsulates the intricacies of database operations, providing a structured and efficient mechanism for performing CRUD (Create, Read, Update, Delete) operations.

2. **Service Layer: Orchestrating Business Logic**

   Building upon the Data Repository, the Service Layer takes center stage. This layer plays a crucial role in encapsulating the business logic associated with data manipulation. It serves as the orchestrator, ensuring that interactions with the Data Repository align with predefined business rules and workflows. By consolidating data-related operations, the Service Layer promotes modularity and maintainability.

3. **Business Logic Manager: Navigating Complexity**

   As complexity increases, the Business Logic Manager layer steps in to manage higher-level business logic. It acts as a centralized hub for implementing intricate business rules, coordinating various operations, and abstracting away complexities from the Service Layer. This tier contributes to a clean and modular design, making it easier to adapt to evolving business requirements.

4. **MediatR in Controllers: Decoupled Communication**

   Embracing the Mediator pattern, the Controllers layer leverages MediatR for handling communication between request on controllers and BussinessLogic layer .

5. **Shared Kernel: Common Functionality**

Introducing a Shared Kernel layer that houses common functionality shared across the application:

- **ResponseApi:**
    - A standardized structure for API responses, promoting consistency and clarity in communication.

- **HttpCodeHelper:**
    - A utility for managing HTTP status codes, ensuring a uniform and meaningful representation of response states.

- **RequestTimeLogging:**
    - Logging the time taken by requests, facilitating performance monitoring and optimization efforts.


## How to Run the Solution Locally

### 1. Install .NET 8 SDK

- Visit [Download .NET 8](https://dotnet.microsoft.com/es-es/download/dotnet/8.0) for general instructions.
- For x86 architecture, use the following link: [SDK x86 Installer](https://dotnet.microsoft.com/es-es/download/dotnet/thank-you/sdk-8.0.100-windows-x86-installer).
- Note: If you have a processor that does not support arm64, avoid using x64.

### 2. If you already have a previous version of .NET SDK

   - a) Check your current version: `dotnet --version`
   - b) Review installed SDKs: `dotnet --list-sdks`
   - c) Set the correct SDK version for this API: `dotnet set sdk 8.0`

### 3. Run API on Localhost.
   - a) Clone project API (not test) on folder SRC
   - b) Execute dotnet build
   - c) Execute dotnet run


Feel free to customize and expand upon these instructions based on your project's specific requirements.

