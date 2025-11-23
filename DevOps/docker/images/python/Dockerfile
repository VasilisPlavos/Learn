# Use an up-to-date slim Ubuntu base image
FROM ubuntu:jammy-20240227

RUN apt-get update -y
RUN apt-get install -y python3 
RUN apt-get install -y python3-pip 

# COPY app app

WORKDIR /app
EXPOSE 8888

CMD ["tail", "-f", "/dev/null"]
# -v $(pwd):/root mounts the current directory


# docker build -t py3app .
# docker run -d -p 3121:8888 -v .\app:/app --name py3app py3app
# docker run -d -p 3121:8888 -v .\app:/app --name py3app --restart always py3app 

