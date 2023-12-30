dotnet new mvc -n kds


mkdir ssl
openssl req -x509 -newkey rsa:4096 -keyout ssl/kds.key -out ssl/kds.crt -days 365
openssl pkcs12 -export -out ssl/certificate.pfx -inkey ssl/kds.key -in ssl/kds.crt
openssl x509 -in ssl/kds.crt -noout -text

docker build -t kds-frontend .
docker run -d -p 8000:8080 --name kds-frontend-container kds-frontend
docker exec -it kds-frontend-container bash
docker logs kds-frontend-container
docker stop kds-frontend-container
docker rm kds-frontend-container
docker rmi kds-frontend



