#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8-windowsservercore-ltsc2019

## Copy fonts 
#COPY Fonts ./Windows/Fonts
## Register the Fonts.
#RUN powershell.exe -NoProfile -Command New-ItemProperty -Path 'HKLM:\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts' -Name 'SimSun "&" NSimSun (TrueType)' -PropertyType String -Value simsun.ttc
#RUN powershell.exe -NoProfile -Command New-ItemProperty -Path 'HKLM:\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts' -Name 'Malgun Gothic (TrueType)' -PropertyType String -Value malgun.ttf
#RUN powershell.exe -NoProfile -Command New-ItemProperty -Path 'HKLM:\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts' -Name 'MS Gothic "&" MS UI Gothic "&" MS PGothic (TrueType)' -PropertyType String -Value msgothic.ttc
## Register the FontLink
#RUN powershell.exe -NoProfile -Command New-ItemProperty -Path 'HKLM:\SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontLink\SystemLink' -Name 'SimSun' -PropertyType MultiString -Value ('MSGOTHIC.TTC,MS UI Gothic','SIMSUN.TTC,SimSun','MALGUN.TTF,Malgun Gothic')
#
ARG source

#WORKDIR /inetpub/fonts
#COPY ${source:-obj/Docker/publish}/fonts .
#RUN FontReg.exe /copy

WORKDIR /inetpub/wwwroot
COPY ${source:-obj/Docker/publish} .

#CMD powershell.exe start C:/inetpub/wwwroot/Fonts/FontReg.exe /copy


RUN powershell.exe Copy-Item -Path C:/inetpub/wwwroot/Fonts/* -Destination C:/Windows/Fonts -Recurse
RUN ./FontReg.exe
# CMD ["FontReg.exe", "/copy"]
