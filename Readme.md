dotnet new mvc -n kds
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet tool install --global dotnet-ef


dotnet ef migrations add Initial

docker build -t kds .
docker run -d -p 80:8080 --name kds-container kds
docker exec -it kds-container /bin/bash


