---
name: audio-video-converter
description: Download YouTube videos/playlists and Beatstars tracks as MP3 audio files. Use when extracting audio from YouTube links, downloading music from playlists, or converting Beatstars beats to MP3.
---

# YouTube & Beatstars Audio Downloader

## 🛠 Installation & Setup

Before using the scripts, ensure your system has Python, Pip, and FFmpeg installed.

### 1. Install System Dependencies
```bash
apt update && apt install -y python3 python3-pip ffmpeg
```

### 2. Install/update yt-dlp:
```bash
pip install -U yt-dlp
```

## YouTube Single Video to MP3

```bash
yt-dlp --extract-audio --audio-format mp3 -o "%(title)s.%(ext)s" --write-thumbnail <VIDEO_URL>
```

## YouTube Playlist to MP3

```bash
yt-dlp --extract-audio --audio-format mp3 -o "%(playlist_index)s - %(title)s.%(ext)s" --write-thumbnail <PLAYLIST_URL>
```

## YouTube Download video with audio

```bash
yt-dlp -f "bv+ba/b" <VIDEO_URL>
# e.g. `yt-dlp -f "bv+ba/b" https://www.youtube.com/watch?v=swXWUfufu2w`
```

### Output Patterns
- `-o "%(title)s.%(ext)s"` → `videotitle.mp3`
- `-o "%(playlist_index)s - %(title)s.%(ext)s"` → `01 - videotitle.mp3`
- `--write-thumbnail` → downloads video/playlist thumbnail

## Beatstars to MP3

1. Extract beat ID from URL (e.g., `18476261` from `https://www.beatstars.com/beat/--60-last-days--light--tyga-type-beat-offset-club-banger-18476261`)

2. Fetch stream URL:
```bash
curl -s "https://main.v2.beatstars.com/beat?id=<BEAT_ID>&fields=details"
```

3. Find `m3u8` URL in response, then convert:
```bash
ffmpeg -i "<M3U8_URL>" output.mp3
```
