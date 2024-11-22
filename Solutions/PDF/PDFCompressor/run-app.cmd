docker build -t pdf-compressor-app .
docker run -d  --name pdf-compressor-app -v .\app:/app pdf-compressor-app
docker wait pdf-compressor-app
start .\app\out
docker rm pdf-compressor-app

@REM the above command can keep the container open if required
@REM docker run -d  --name pdf-compressor-app -v .\app:/app pdf-compressor-app  /bin/bash -c "echo 'Hello World'; sleep infinity"
