# Use open-webui main as the base image
FROM ghcr.io/open-webui/open-webui:main


# Update the package list and install prerequisites
RUN apt-get update && apt-get install -y \
    curl \
    wget \
    && apt-get clean

# install ollama
RUN curl -fsSL https://ollama.com/install.sh | sh

# NOTE: run the above commands to build and run
# docker build -t open-webui-ollama-image .
# docker run -d -p 3000:8080 --add-host=host.docker.internal:host-gateway -v open-webui:/app/backend/data --name open-webui --restart always open-webui-ollama-image