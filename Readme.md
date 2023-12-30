docker-compose up -d

docker-compose build

------------------------------------------------------------
aws ecr create-repository --repository-name kds-backend-container
aws ecr get-login-password --region ap-southeast-2 | docker login --username AWS --password-stdin {repositoryUri}
docker tag kds-backend-container {repositoryUri}
docker push {repositoryUri}

docker pull {repositoryUri}
docker run -d -p 9000:443 -v /app/Database:/kds-backend/Database {repositoryUri}
------------------------------------------------------------
aws ecr create-repository --repository-name kds-frontend-container
aws ecr get-login-password --region ap-southeast-2 | docker login --username AWS --password-stdin {repositoryUri}
docker tag kds-frontend-container {repositoryUri}
docker push {repositoryUri}

docker pull {repositoryUri}
docker run -d -p 8000:443 -p 8080:80 {repositoryUri}
