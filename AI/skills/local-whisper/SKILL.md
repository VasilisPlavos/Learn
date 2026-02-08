---
name: local-whisper
description: Local speech-to-text using OpenAI Whisper. Runs fully offline after model download. High quality transcription with multiple model sizes. Use when transcribing audio files, voice notes, or extracting text from video/audio.
metadata: {"clawdbot":{"emoji":"🎙️","requires":{"bins":["ffmpeg"]}}}
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
```

## Models

| Model | Size | Notes |
|-------|------|-------|
| `tiny` | 39M | Fastest |
| `base` | 74M | **Default** |
| `small` | 244M | Good balance |
| `turbo` | 809M | Best speed/quality |
| `large-v3` | 1.5GB | Maximum accuracy |

## Options

- `--model/-m` — Model size (default: base)
- `--language/-l` — Language code (auto-detect if omitted)
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