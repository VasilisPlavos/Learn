FROM ubuntu:jammy-20240227

RUN apt-get update && apt-get install -y --no-install-recommends \
    python3 \
    pip \
    ghostscript \
    && apt-get clean

COPY app /app
WORKDIR /app

# RUN python3 -m pip install -r requirements.txt
CMD ["python3", "app.py"]