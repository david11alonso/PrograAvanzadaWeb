FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /source

# Copy everything else and build
COPY Solution/ Solution/
COPY Solution.API_W/ Solution.API_W/
COPY Solution.BS/ Solution.BS/
COPY Solution.DAL/ Solution.DAL/
COPY Solution.DAL.EF/ Solution.DAL.EF/
COPY Solution.DAL.Repository/ Solution.DAL.Repository/
COPY Solution.DO/ Solution.DO/
WORKDIR /source/Solution
FROM build AS publish
RUN dotnet build -c release -o /app

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Solution.API.dll"]
