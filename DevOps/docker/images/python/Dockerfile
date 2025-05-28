# Use an up-to-date slim Ubuntu base image
FROM ubuntu:jammy-20240227

RUN apt-get update && apt-get upgrade -y
RUN apt-get install -y python3 
RUN apt-get install -y python3-pip 

# COPY app app

WORKDIR /app
EXPOSE 8888
CMD ["python", "-i"]

# If you want to keep the container alive without the interactive shell, you can use something like:
# CMD ["tail", "-f", "/dev/null"]
# -v $(pwd):/root mounts the current directory


# docker build -t my-python-env .
# docker run -d -p 3220:8888 -v $(pwd):/app --name my-python-env --restart always my-python-env 

