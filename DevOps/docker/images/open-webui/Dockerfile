# Use open-webui main as the base image
# FROM ghcr.io/open-webui/open-webui:main

# Use open-webui with ollama integrated
FROM ghcr.io/open-webui/open-webui:ollama


# Update the package list and install prerequisites
RUN apt-get update && apt-get install -y --no-install-recommends \
    curl \
    wget \
    && apt-get clean

# Install Ollama (a script hosted on the website)
RUN curl -fsSL https://ollama.com/install.sh | sh

# Use this environment varriable to serve OLLAMA using the same container
# ENV USE_OLLAMA_DOCKER=true

# Use this to dissable AUTH (not recommented!)
ENV WEBUI_AUTH=false

# Use this environment varriable to set custom OLLAMA server
# ENV OLLAMA_BASE_URL=http://localhost:11434 OPENAI_API_BASE_URL=

# NOTE: run the above commands to build and run
# docker build -t open-webui-ollama:dev .
# docker run -d -p 3100:8080 -v ${env:USERPROFILE}/ai/models:/root/.ollama/models --name open-webui-ollama-dev --restart always open-webui-ollama:dev


# with this param you can have a shared volume across the images, sharing the user data
# -v open-webui:/app/backend/data
# docker run -d -p 3100:8080 -v ${env:USERPROFILE}/ai/models:/root/.ollama/models -v open-webui-no-auth:/app/backend/data --name open-webui-ollama-dev --restart always open-webui-ollama:dev