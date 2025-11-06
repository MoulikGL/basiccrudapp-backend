FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build  # Use .NET SDK image to build the application
WORKDIR /src                                    # Set working directory inside the container
COPY BasicCrudApp.sln ./                        # Copy solution file for dependency restore
COPY BasicCrudApp/*.csproj ./BasicCrudApp/      # Copy project file for restore
RUN dotnet restore                              # Restore NuGet dependencies

COPY . .                                        # Copy all source code into the container
WORKDIR /src/BasicCrudApp                       # Navigate to project directory
RUN dotnet publish -c Release -o /app/publish   # Build and publish app to /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime  # Use runtime image for running app
WORKDIR /app                                         # Set working directory
COPY --from=build /app/publish .                     # Copy published app from build stage
ENTRYPOINT ["dotnet", "BasicCrudApp.dll"]            # Define entry point to run the app