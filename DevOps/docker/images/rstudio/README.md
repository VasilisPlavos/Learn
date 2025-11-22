# Rstudio server

```console
# username: rstudio password: password
docker run -p 3116:8787 --name rstudio -e PASSWORD=password rocker/rstudio:4.5.1
```

## Sources

* [https://davetang.org/muse/2021/04/24/running-rstudio-server-with-docker/](https://davetang.org/muse/2021/04/24/running-rstudio-server-with-docker/)
