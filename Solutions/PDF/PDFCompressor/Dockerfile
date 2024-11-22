FROM ubuntu:jammy-20240227

RUN apt-get update
RUN apt-get -y install poppler-utils python3 pip ghostscript

COPY app /app
WORKDIR /app

# RUN python3 -m pip install -r requirements.txt
CMD ["python3", "app.py"]