**Everest Code Challenge – Delivery Estimate Solution**

This repository contains my solution for the Everest Engineering Code Challenge, consisting of two independent problem statements:

Delivery Cost Estimate with Offers

Delivery Time Estimate with Offers

The solution is structured as a .NET multi-project setup using console applications, shared class libraries, and xUnit tests.

**Solution Structure
Main Solution:**
Everest.DeliveryEstimateSolution.sln

**Projects Included**
DeliveryCostEstimate -	Console App (.NET) -	Calculates delivery cost with offer codes applied.
DeliveryTimeEstimate -	Console App (.NET) -	Computes estimated delivery time based on distance, speed, and rules.
DeliveryEstimateLibrary	- .NET Class Library - Contains shared logic used by both cost & time estimation apps.
DeliveryEstimateTests	- xUnit Test Project	- Unit tests validating core logic and edge cases.

**Tech Stack**
.NET 8
Console Applications
xUnit for unit testing
C#
.NET Class Library for shared business logic

📦 Shared Library Usage
The folder DeliveryEstimateLibrary contains reusable components shared by:
DeliveryCostEstimate
DeliveryTimeEstimate
Common logic includes:
Offer-related validations
Mathematical calculation helpers
Shared models/constants
This avoids duplication and ensures both solutions behave consistently.

**How to Build & Run the Project**

Before running anything, ensure .NET SDK is installed.

1. Restore Dependencies
dotnet restore

2. Build All Projects
dotnet build

3. Run Delivery Cost Estimate
dotnet run --project ./Everest.DeliveryCostEstimate

4. Run Delivery Time Estimate
dotnet run --project ./Everest.DeliveryTimeEstimate

**Running Unit Tests**
The test project is built using xUnit.

Run all tests
dotnet test

Run tests for a specific project
dotnet test ./Everest.UnitTest
