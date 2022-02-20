SETLOCAL EnableExtensions
@REM filename here
set EXE=ffmpeg.exe 

:check
FOR /F %%x IN ('tasklist /NH /FI "IMAGENAME eq %EXE%"') DO IF %%x == %EXE% (
  echo %EXE% is Running
  sleep 10
  GOTO check
)

@REM if task stop running shutdown
shutdown /s /t 60