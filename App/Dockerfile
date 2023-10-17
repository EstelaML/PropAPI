# create the dockerfile for the app that is a c# api:

# use the microsoft/dotnet image as a base
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
COPY . /App
WORKDIR /App

RUN ["dotnet", "restore"]
RUN ["dotnet", "build"]

ENTRYPOINT ["dotnet", "run"]
