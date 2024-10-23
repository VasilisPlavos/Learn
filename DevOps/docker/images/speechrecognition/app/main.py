import re
from fastapi import BackgroundTasks, FastAPI
from fastapi.responses import RedirectResponse
import subprocess
import speech_recognition as sr
import os
import json

# cmd: fastapi dev
app = FastAPI()

def save_file(folderPath, fileName, jsonFile):
    if not os.path.isdir(folderPath):
        os.makedirs(folderPath)
    filePath = os.path.join(folderPath, fileName)
    file = open(filePath, 'w')
    file.write(jsonFile)
    file.close()

def save_file_by_channel(channel, id, jsonFile):
    folderPath = f"./{channel}/{id}/"
    fileName = "index.json"
    save_file(folderPath, fileName, jsonFile)


def get_audio(folderPath, youtubeUrl):
    audioFile = f"{folderPath}audio.wav"
    cmd = f"yt-dlp --extract-audio --audio-format wav -o {audioFile} {youtubeUrl}"
    subprocess.run(f"{cmd}", shell=True)
    return audioFile

def get_text(audioFile, language="en"):
    r = sr.Recognizer()
    with sr.AudioFile(audioFile) as source:
        data = r.record(source)
        text = r.recognize_whisper(audio_data=data, language=language)
        return text

def run_process_in_background(channel, id):
    folderPath = f"./{channel}/{id}/"
    fileName = "index.json"
    if (channel != "yt"):
        save_file(folderPath, fileName, json.dumps({ "id": id, "status": f'{channel} is not supported' }))
    
    save_file(folderPath, fileName, json.dumps({ "id": id, "status": "generating audio file" }))
    youtubeUrl = f'https://youtu.be/{id}'
    audioFile = get_audio(folderPath, youtubeUrl)
    save_file(folderPath, fileName, json.dumps({ "id": id, "status": "generating text" }))
    text = get_text(audioFile)
    save_file(folderPath, fileName, json.dumps({ "id": id, "status": "done", "text": text }))
    os.remove(audioFile)


def check_status(channel, id):
    folderPath = f"./{channel}/{id}/"
    fileName = "index.json"
    filePath = os.path.join(folderPath, fileName)
    file_context = ''
    if os.path.exists(filePath):
        with open(filePath) as json_data:
            file_context = json.load(json_data)
    else:
        file_context = { "channel": channel, "id": id, "status": "start" }
        save_file(folderPath, fileName, json.dumps(file_context))
    return file_context

def get_youtube_id(url):
    try:
        match = re.search(r"(?:v=|\/)([0-9A-Za-z_-]{11}).*", url)
        return match.group(1) if match else ""
    except:
        return ""

@app.get('/')
async def read_root(url: str | None = None):
    youtubeId = get_youtube_id(url)
    if (youtubeId != ""):
        response = RedirectResponse(url=f'/yt/{youtubeId}')
        return response
    return "Try something like http://localhost:3300/?url=https://www.youtube.com/watch?v=swXWUfufu2w?version=3&amp;hl=en_US"

@app.get("/yt/{id}")
async def yt_id(id: str, background_tasks: BackgroundTasks):
    channel = 'yt'
    response = check_status(channel, id)
    status = response["status"]
    if (status == "start"):
        background_tasks.add_task(run_process_in_background, "yt", id)
        response = { "channel": channel, "id": id, "status": "work in progress, check later" }
        save_file_by_channel(channel, id, json.dumps(response))
    return response