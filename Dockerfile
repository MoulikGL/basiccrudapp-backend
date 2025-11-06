# Use .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Set working directory inside the container
WORKDIR /src

# Copy solution file for dependency restore
COPY BasicCrudApp.sln ./

# Copy project file for restore
COPY BasicCrudApp/*.csproj ./BasicCrudApp/

# Restore NuGet dependencies
RUN dotnet restore

# Copy all source code into the container
COPY . .

# Navigate to project directory
WORKDIR /src/BasicCrudApp

# Build and publish app to /app/publish
RUN dotnet publish -c Release -o /app/publish

# Use runtime image for running app
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime

# Set working directory
WORKDIR /app

# Copy published app from build stage
COPY --from=build /app/publish .

# Define entry point to run the app
ENTRYPOINT ["dotnet", "BasicCrudApp.dll"]