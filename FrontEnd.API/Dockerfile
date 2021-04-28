FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /source

# Copy everything else and build
COPY . .
WORKDIR /source/
FROM build AS publish
RUN dotnet build -c release -o /app
RUN mkdir /app/wwwroot/
RUN cp -r wwwroot/* /app/wwwroot/

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
RUN ls
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Frontend.API.dll"]
