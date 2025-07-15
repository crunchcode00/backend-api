FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app

# Copy everything and build
COPY . ./
RUN dotnet restore
RUN dotnet build -c Release
RUN dotnet publish -c Release -o out

# Set up runtime
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

# Run the app
CMD ["dotnet", "out/MentalHealthCompanion.API.dll"]
