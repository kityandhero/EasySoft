﻿#docker build -t  application_docker_deploy

#dotnet restore
dotnet publish -c Release

#cd ./bin/Release/net6.0
Set-Location ./bin/Release/net6.0/publish

docker compose build
docker compose up

Set-Location ../../../../