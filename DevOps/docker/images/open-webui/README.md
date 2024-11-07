# Open WebUI

## Open WebUI with Ollama version - the AIO command

`docker run -d -p 3100:8080 -v ${env:USERPROFILE}/ai/models:/root/.ollama/models -e WEBUI_AUTH=false --name open-webui-ollama-dev --restart always ghcr.io/open-webui/open-webui:ollama`

### Breakdown the parameters

- `${env:USERPROFILE}/ai/models`: Is the localfolder that I am storing the LLMs. It is usefull because I can reuse these models through dockers
- `/root/.ollama/models`: Is the directory that Open WebUI storing the LLMs
- `WEBUI_AUTH=false`: Since I am running these models on localhost there is no need for AUTH.

### Known issue

* Sometimes Open WebUI does not have the last verion of Ollama installed!
