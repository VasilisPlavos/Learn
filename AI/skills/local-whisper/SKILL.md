---
name: local-whisper
description: Local speech-to-text using OpenAI Whisper. Runs fully offline after model download. High quality transcription with multiple model sizes. Use when transcribing audio files, voice notes, or extracting text from video/audio.
metadata: { "clawdbot": { "emoji": "🎙️", "requires": { "bins": ["ffmpeg"] } } }
---

# Local Whisper STT

Local speech-to-text using OpenAI's Whisper. **Fully offline** after initial model download.

## Usage

```bash
# Basic transcription
python3 /app/skills/local-whisper/scripts/transcribe.py audio.wav

# Better model
python3 /app/skills/local-whisper/scripts/transcribe.py audio.wav --model turbo

# With timestamps
python3 /app/skills/local-whisper/scripts/transcribe.py audio.wav --timestamps --json

# With standard SRT subtitles
python3 /app/skills/local-whisper/scripts/transcribe.py audio.wav --model turbo --language English --timestamps --srt
```

## Models

| Model      | Parameters | VRAM Required | Notes              |
| ---------- | ---------- | ------------- | ------------------ |
| `tiny`     | 39M        | ~1 GB         | Fastest            |
| `base`     | 74M        | ~1 GB         | **Default**        |
| `small`    | 244M       | ~2 GB         | Good balance       |
| `turbo`    | 809M       | ~6 GB         | Best speed/quality |
| `large-v3` | 1.5GB      | ~10 GB        | Maximum accuracy   |

## Options

- `--model/-m` — Model size (default: base)
- `--language/-l` — Language code (auto-detect if omitted)
  --task : Use transcribe (default) or translate (to English).
  --output_format, -f : Choose format (srt, vtt, txt, json, tsv, all).
--word_timestamps : Boolean; adds precise timing for every word.
--output_dir, -o : Directory to save resulting files.

- `--timestamps/-t` — Include word timestamps
- `--json/-j` — JSON output
- `--quiet/-q` — Suppress progress

## Setup

Requires `ffmpeg` and Python dependencies:

```bash
pip install click openai-whisper torch
```

For CPU-only (smaller install):

```bash
pip install click openai-whisper torch --index-url https://download.pytorch.org/whl/cpu
```
