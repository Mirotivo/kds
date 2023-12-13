1. dotnet new webapi
2. dotnet restore
3. dotnet build
4. dotnet run
5. dotnet publish -c Release
6. dotnet ef migrations add ...
7. dotnet ef database update



docker build -t kds-backend .
docker run -d -p 9000:8080 -v ${PWD}/Database:/kds-backend/Database --name kds-backend-container kds-backend
docker exec -it kds-backend-container bash
docker logs kds-backend-container
docker stop kds-backend-container
docker rm kds-backend-container
docker rmi kds-backend
