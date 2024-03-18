# Use the official .NET SDK image as a build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0.201-jammy AS build
WORKDIR /app

# Copy the remaining source code and build the application
COPY . ./
RUN dotnet build -c Release "./AudioApp/AudioApp.sln"

# Publish the application to a publish folder
RUN dotnet publish -c Release -o /app/publish "./AudioApp/AudioApp.sln"

# Use the official ASP.NET Core Runtime image as the runtime stage
FROM mcr.microsoft.com/dotnet/sdk:8.0.201-jammy AS runtime
WORKDIR /app

# **Install ffmpeg during image build**
RUN apt-get update && apt-get install -y ffmpeg

# Copy the published application from the build stage
COPY --from=build /app/publish .

# Expose port 80 for HTTP traffic
EXPOSE 80

# Run the application
ENTRYPOINT ["dotnet", "AudioApp.dll"]
