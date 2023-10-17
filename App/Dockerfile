# create the dockerfile for the app that is a c# api:

# use the microsoft/dotnet image as a base
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

# set the working directory to /app
WORKDIR /app

# copy the current directory contents into the container at /app
COPY . /app

# expose port 5000
EXPOSE 5000

# run the command 'dotnet run' when the container launches
CMD ["dotnet", "run"]
