dotnet new mvc -n kds
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet tool install --global dotnet-ef


dotnet ef migrations add Initial

docker build -t kds-frontend .
docker run -d -p 8000:8080 --name kds-frontend-container kds-frontend
docker exec -it kds-frontend-container bash
docker logs kds-frontend-container
docker stop kds-frontend-container
docker rm kds-frontend-container
docker rmi kds-frontend


