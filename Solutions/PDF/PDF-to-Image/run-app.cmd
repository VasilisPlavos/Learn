docker build -t pdf-to-image-app .
docker run -d  --name pdf-to-image-app -v .\app:/app pdf-to-image-app
docker wait pdf-to-image-app
start .\app
docker rm pdf-to-image-app

@REM the above command can keep the container open if required
@REM docker run -d  --name pdf-to-image-app -v .\app:/app pdf-to-image-app  /bin/bash -c "echo 'Hello World'; sleep infinity"
