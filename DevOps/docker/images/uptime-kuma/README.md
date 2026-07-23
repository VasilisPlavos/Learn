
# Uptime Kuma

## Install

```console
docker run -d --restart=always -p 3110:3001 -v ./uptime-kuma-data:/app/data --name uptime-kuma louislam/uptime-kuma:2
```

## Update

```console
docker pull louislam/uptime-kuma:2
docker stop uptime-kuma
docker rm uptime-kuma
docker run -d --restart=always -p 3110:3001 -v ./uptime-kuma-data:/app/data --name uptime-kuma louislam/uptime-kuma:2
```
