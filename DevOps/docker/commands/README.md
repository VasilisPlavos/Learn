docker build .
docker run -d -p 5000:5000 -p 5004:80 sha256:47f0468d4739e60be80ecdbe79ce061174dbb6bfa5dfa42c

# run docker, keeping it open and open the cmd of docker
docker run -it mcr.microsoft.com/windows/servercore:ltsc2022 cmd.exe

# run new docker without exiting
docker run --hostname=476db36b3104 --restart=no -t -d mcr.microsoft.com/windows/servercore:ltsc2022