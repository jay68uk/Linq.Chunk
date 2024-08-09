# Base image for building the project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["Linq.Chunk.csproj", "Linq.Chunk/"]
RUN dotnet restore "Linq.Chunk/Linq.Chunk.csproj"

# Copy the remaining files and build the project
COPY . .
WORKDIR "/src"
RUN dotnet build "Linq.Chunk.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Linq.Chunk.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage for running the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY . .

# Set up the entry point for running the benchmarks
ENTRYPOINT ["dotnet", "Linq.Chunk.dll", "--filter", "*", "--no-build"] 