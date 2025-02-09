FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet build -c Release

RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

RUN dotnet ef database update --verbose

# Publish the application
RUN dotnet publish -c Release -o /app/publish 

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

RUN apt-get update && apt-get install -y curl

ENV ASPNETCORE_URLS=http://+:5001

CMD ["dotnet", "PatientAPI.dll"]
