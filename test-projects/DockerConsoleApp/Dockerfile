#choose base image
FROM mcr.microsoft.com/dotnet/runtime:5.0

#copy in
COPY publish/ /app

#cmd to start container
CMD dotnet /app/DockerConsoleApp.dll


#dotnet publish -o publish
#docker build -t docker-console .
#docker run docker-console