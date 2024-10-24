# Use open-webui main as the base image
# FROM ollama/ollama
FROM ollama/ollama:0.3.12

# Update the package list and install prerequisites
RUN apt-get update && apt-get install -y --no-install-recommends \
    curl \
    wget \
    && apt-get clean

# Install Ollama (a script hosted on the website)
# RUN curl -fsSL https://ollama.com/install.sh | sh

# Optionally, you can add this line to ensure Ollama works (uncomment if needed)
# RUN ollama serve & ollama run llama3.2:1b-text-q2_K

# Run the specified model (ensure the model name and version are correct)
# RUN ollama run llama3.2:1b-text-q2_K

# docker build -t ollama-local:latest .
# docker run -d -v ollama:/root/.ollama -p 3200:11434 --name ollama-local ollama-local:latest


# docker run -d -v ollama:/root/.ollama -p 3200:11434 --name ollama-1 ollama/ollama
