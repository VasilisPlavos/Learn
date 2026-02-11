---
name: local-whisper
description: Local speech-to-text using OpenAI Whisper. Runs fully offline after model download. High quality transcription with multiple model sizes. Use when transcribing audio files, voice notes, or extracting text from video/audio.
---

# Local Whisper STT

Local speech-to-text using OpenAI's Whisper. **Fully offline** after initial model download.

## Available Models and Languages

There are six model sizes, four with English-only versions, offering speed and accuracy tradeoffs. Relative speeds are approximate (measured on an A100 with English speech) and will vary depending on language, speaking speed, and hardware.

| Size | Parameters | English-only | Multilingual | Required VRAM | Relative Speed |
|:------:|:----------:|:------------:|:------------:|:-------------:|:--------------:|
| tiny | 39 M | `tiny.en` | `tiny` | ~1 GB | ~10x |
| base | 74 M | `base.en` | `base` | ~1 GB | ~7x |
| small | 244 M | `small.en` | `small` | ~2 GB | ~4x |
| medium | 769 M | `medium.en` | `medium` | ~5 GB | ~2x |
| large | 1550 M | N/A | `large` | ~10 GB | 1x |
| turbo | 809 M | N/A | `turbo` | ~6 GB | ~8x |

**Choosing a model:**

- The `.en` models perform better for English-only use, especially `tiny.en` and `base.en`. The difference is less significant for `small.en` and `medium.en`.
- `turbo` is an optimized version of `large-v3` offering fast transcription with minimal accuracy loss — a great default for most transcription tasks.
- `large` (i.e. `large-v3`) provides maximum accuracy across all languages.

**Language support:** Whisper supports dozens of languages with auto-detection. Performance varies by language. See [tokenizer.py](https://github.com/openai/whisper/blob/main/whisper/tokenizer.py) for the full list of supported languages and [the paper](https://arxiv.org/abs/2212.04356) for per-language accuracy benchmarks.

## Command-Line Usage

### Basic transcription

```bash
# Transcribe one or more files (defaults to turbo model)
whisper audio.flac audio.mp3 audio.wav --model turbo
```

### Non-English transcription

Specify the language for better accuracy on non-English audio:

```bash
whisper japanese.wav --language Japanese
```

### Translation to English

Use one of the multilingual models (`tiny`, `base`, `small`, `medium`, `large`) for translation. **Do not use `turbo` for translation** — it is not trained for translation tasks and will return the original language even if `--task translate` is specified.

```bash
whisper japanese.wav --model medium --language Japanese --task translate
```

### Output formats

```bash
# Standard SRT subtitles
whisper audio.wav --model turbo --output_format srt

# JSON with word-level timestamps
whisper audio.wav --model turbo --word_timestamps True --output_format json

# Plain text
whisper audio.wav --output_format txt

# All formats at once
whisper audio.wav --output_format all --output_dir ./output
```

### View all options

```bash
whisper --help
```

## CLI Options Reference

| Option | Description |
|--------|-------------|
| `--model` / `-m` | Model size: `tiny`, `base`, `small`, `medium`, `large`, `turbo` (default: `turbo`). Append `.en` for English-only variants where available. |
| `--language` / `-l` | Language code or name (auto-detected if omitted). |
| `--task` | `transcribe` (default) or `translate` (translate to English). Use a multilingual model — **not `turbo`** — for translation. |
| `--output_format` / `-f` | Output format: `srt`, `vtt`, `txt`, `json`, `tsv`, or `all`. |
| `--word_timestamps` | Enable precise word-level timing in output. |
| `--output_dir` / `-o` | Directory to save output files. |
| `--timestamps` / `-t` | Include word timestamps in output. |
| `--quiet` / `-q` | Suppress progress output. |

## Setup

Requires `ffmpeg` and Python dependencies:

```bash
pip install click openai-whisper torch
```

For CPU-only (smaller install):

```bash
pip install click openai-whisper torch --index-url https://download.pytorch.org/whl/cpu
```
