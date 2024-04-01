# linux
FROM mcr.microsoft.com/dotnet/aspnet:6.0.22-jammy-amd64
RUN apt-get update && apt-get install -y wkhtmltopdf fontconfig 

# linux-ubuntu
# FROM ubuntu:latest
# FROM ubuntu:jammy-20240227
# RUN apt-get update && apt-get install -y wkhtmltopdf fontconfig dotnet6

# FROM mcr.microsoft.com/dotnet/aspnet:6.0-nanoserver-ltsc2022
# FROM chocolatey/choco:latest-windows
# RUN choco install wkhtmltopdf -y

# windows
# FROM mcr.microsoft.com/windows/servercore:ltsc2022
# COPY app5 app
# RUN C:/app/aspnetcore-runtime-6.0.20-win-x64.exe /install /quiet /norestart
# RUN C:/app/dotnet-runtime-6.0.20-win-x64.exe /install /quiet /norestart

COPY app5 app
RUN cp -r app/fonts /usr/share/fonts/truetype
RUN chmod 644 /usr/share/fonts/truetype/*
RUN fc-cache -f -v


# WORKDIR /app
# COPY . .
# RUN cp -r fonts /usr/share/fonts/truetype
# RUN chmod 644 /usr/share/fonts/truetype/*
# RUN fc-cache -f -v


# ENV PORT 443
# EXPOSE 443

# ENV PORT 5000
# EXPOSE 5000

# ENV PORT 5001
# EXPOSE 5001

ENV PORT 80
EXPOSE 80

ENV ASPNETCORE_URLS "http://*:${PORT}"

WORKDIR /app
ENTRYPOINT ["dotnet", "SimpleApi.dll"]