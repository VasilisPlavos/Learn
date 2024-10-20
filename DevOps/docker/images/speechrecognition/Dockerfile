# Use an up-to-date slim Ubuntu base image
FROM ubuntu:jammy-20240227

# Update the package list and install prerequisites
RUN apt-get update && apt-get install -y --no-install-recommends \
    curl \
    wget \
    python3 \
    python3-pip \
    ffmpeg \
    && apt-get clean

RUN apt-get install -y jupyter

RUN pip install --no-cache-dir --upgrade pip && \
pip install --no-cache-dir SpeechRecognition[whisper-local] yt-dlp --ignore-installed

# Set working directory
WORKDIR /home/user

# Increase user file descriptor limit (optional for some .NET applications)
# RUN ulimit -n 65535


# Expose Jupyter notebook port (default: 8888)
EXPOSE 8888

# Run jupyter notebook on container start
# jupyter notebook --allow-root --port=8888 --ip=0.0.0.0
CMD ["jupyter", "notebook", "--allow-root", "--port=8888", "--ip=0.0.0.0"]

# docker build -t temp-py .
# docker run -d -p 3200:8888 -v C:/Users/vplav/Gits/monopoly-personal/examples/AudioVideoConverter:/home/user --name temp-py --restart always temp-py


# docker build -t youtube-to-text:latest .
# docker run --hostname=476db36b3104 --restart=no -d -v C:/Users/vplav/Gits/monopoly-personal/examples/AudioVideoConverter:/home/user --name youtube-to-text youtube-to-text:latest

# - `docker run --hostname=476db36b3104 --restart=no -t -d ubuntu:jammy-20240227`