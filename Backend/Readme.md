dotnet tool install --global dotnet-ef

1. dotnet new webapi
2. dotnet restore
3. dotnet build
4. dotnet run
5. dotnet publish -c Release
6. dotnet ef migrations add ...
7. dotnet ef database update
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design


Install-Module -Name OpenSSL
mkdir ssl
openssl req -x509 -newkey rsa:4096 -keyout ssl/kds.key -out ssl/kds.crt -days 365
openssl pkcs12 -export -out ssl/certificate.pfx -inkey ssl/kds.key -in ssl/kds.crt
openssl x509 -in ssl/kds.crt -noout -text

docker build -t kds-backend .
docker run -d -p 9000:8080 -v ${PWD}/Database:/kds-backend/Database --name kds-backend-container kds-backend
docker exec -it kds-backend-container bash
docker logs kds-backend-container
docker stop kds-backend-container
docker rm kds-backend-container
docker rmi kds-backend
