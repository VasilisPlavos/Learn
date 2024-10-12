########################################################################
# use open-webui main image and install ollama from website

# Use open-webui main as the base image
# FROM ghcr.io/open-webui/open-webui:main


# Update the package list and install prerequisites
# RUN apt-get update && apt-get install -y --no-install-recommends \
#     curl \
#     wget \
#     && apt-get clean

# Install Ollama (a script hosted on the website)
# RUN curl -fsSL https://ollama.com/install.sh | sh

########################################################################



########################################################################
#
# Use open-webui with ollama integrated
FROM ghcr.io/open-webui/open-webui:ollama
#
########################################################################

# Use this environment varriable to serve OLLAMA using the same container
# ENV USE_OLLAMA_DOCKER=true

# Use this to dissable AUTH (not recommented!)
ENV WEBUI_AUTH=false

# Use this environment varriable to set custom OLLAMA server
# ENV OLLAMA_BASE_URL=http://localhost:11434 OPENAI_API_BASE_URL=

# NOTE: run the above commands to build and run
# docker build -t open-webui-ollama:dev .
# docker run -d -p 3100:8080 -v ${env:USERPROFILE}/ai/models:/root/.ollama/models --name open-webui-ollama-dev --restart always open-webui-ollama:dev

# -v open-webui:/app/backend/data
# docker run -d -p 3100:8080 -v ${env:USERPROFILE}/ai/models:/root/.ollama/models -v open-webui-no-auth:/app/backend/data --name open-webui-ollama-dev --restart always open-webui-ollama:dev


# the AIO commands
# docker run -d -p 3100:8080 -v ${env:USERPROFILE}/ai/models:/root/.ollama/models -e WEBUI_AUTH=false --name open-webui-ollama-dev --restart always ghcr.io/open-webui/open-webui:ollama


#########################################################################
# Spare code bellow
#########################################################################
# ENV PORT 11434
# EXPOSE 11434



# docker build -t open-webui-ollama-image:main .
# docker run -d -p 3000:8080 --add-host=host.docker.internal:host-gateway -v open-webui:/app/backend/data --name open-webui --restart always open-webui-ollama-image:main

# docker run -d -p 1111:8080 -v ${pwd}/shared/models:/root/.ollama/models  --name open-webui-ollama-image-temp --restart always open-webui-ollama-image-temp:main
# docker run -d -p 1111:8080 -v %USERPROFILE%/ai/models:/root/.ollama/models  --name open-webui-ollama-image-temp --restart always open-webui-ollama-image-temp:main



# docker run -d -p 3000:8080 --gpus=all -v ollama:/root/.ollama -v open-webui:/app/backend/data --name open-webui --restart always ghcr.io/open-webui/open-webui:ollama
# docker run -d -p 1222:8080 -v ollama:/root/.ollama -v open-webui:/app/backend/data --name open-webui --restart always ghcr.io/open-webui/open-webui:ollama
# docker run -d -p 1222:8080 -v ollama:/root/.ollama --name open-webhjkui --restart always ghcr.io/open-webui/open-webui:ollama

# docker run -d -p 1111:8080 -e USE_OLLAMA_DOCKER=true -e WEBUI_AUTH=false  --name open-webui-ollama-image-temp --restart always open-webui-ollama-image-temp:main