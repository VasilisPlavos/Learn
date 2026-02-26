# SageMath

## SageMath with Jupyter

`docker run -p 3115:8888 --name sagemath10.7 -v ${env:USERPROFILE}/docker/volumes/sage-data:/home/sage/jupyter/data sagemath/sagemath:10.7 sage-jupyter`

## How to open

For some reason that I cannot explain, in order to open it for first time, open the docer container and click in the link with the ip address. It will open in your browser, there, change the port from 8888 to 3115, hit enter and go!

## PDF pandoc install

```bash
sudo apt-get install pandoc
```

### Sources

* [https://hub.docker.com/r/sagemath/sagemath](https://hub.docker.com/r/sagemath/sagemath)
* [https://www.sagemath.org/sagebook/english.html](https://www.sagemath.org/sagebook/english.html)
* <https://doc.sagemath.org/html/en/reference/spkg/pandoc.html>
