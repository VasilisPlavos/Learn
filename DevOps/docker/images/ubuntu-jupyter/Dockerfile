# Use an up-to-date slim Ubuntu base image
FROM ubuntu:jammy-20240227

# Update package lists
RUN apt-get update && apt-get upgrade -y

ENV DEBIAN_FRONTEND noninteractive

RUN apt update && apt install -y tcl

RUN apt-get install -y dotnet-sdk-8.0 
RUN apt-get install -y python3 
RUN apt-get install -y python3-pip 
RUN apt-get install -y jupyter
RUN apt-get install -y ffmpeg

# RUN apt-get install -y snapd
# RUN snap install --classic code
# dotnet tool install --global Microsoft.dotnet-interactive --version 1.0.155302
# dotnet tool install --global dotnet-try
# dotnet interactive jupyter install
RUN dotnet tool install --global Microsoft.dotnet-interactive --version 1.0.155302
# RUN dotnet tool install --global dotnet-try
# RUN dotnet interactive jupyter install

# Set working directory
WORKDIR /home/user

# Increase user file descriptor limit (optional for some .NET applications)
# RUN ulimit -n 65535

# Copy vscode settings (optional)
# You can copy your preferred vscode settings.json here
# COPY vscode/settings.json .vscode/settings.json

# Expose Jupyter notebook port (default: 8888)
EXPOSE 8888

# Run jupyter notebook on container start
# jupyter notebook --allow-root --port=8888 --ip=0.0.0.0
CMD ["jupyter", "notebook", "--allow-root", "--port=8888", "--ip=0.0.0.0"]

# Optional: Change the default user (security best practice is to not use root)
# USER vscode

# Run docker container, with a local folder
# docker build -t ubuntu-jupyter .
# docker run -d -p 3111:8888 -v C:/Users/vplav/Gits/Learned/Python/jupy:/root/jupy --name ubuntu-jupyter ubuntu-jupyter

# FORGET EVERYTHING ABOVE, USE THE FOLLOWING INSTEAD
# docker run -p 3111:8888 -v C:/Users/vplav/Gits/Learned/Python/jupy:/home/jovyan/jupy --name jupyter jupyter/base-notebook start-notebook.sh --NotebookApp.token='' --NotebookApp.password=''
