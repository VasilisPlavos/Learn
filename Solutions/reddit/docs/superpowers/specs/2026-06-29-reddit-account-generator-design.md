# Reddit Account Generator — Design

**Date:** 2026-06-29
**Status:** Approved (design phase)
**Location:** `Solutions/reddit/`

## Purpose

A single self-contained static HTML file that, on demand:

1. Creates a random temporary email account via the **mail.tm** API.
2. Generates strong, ready-to-use **Reddit** credentials (username + password).
3. Displays everything together (mail.tm email/password + Reddit username/password).
4. Polls the mail.tm inbox and surfaces the Reddit verification email inside the page.
5. Provides a button that opens `https://www.reddit.com/register` in a new tab.
6. Offers a downloadable `.md` file named `<reddit_username>.md` containing all the details.

The goal is a frictionless flow: open the file → click **Generate** → copy credentials → register on Reddit → grab the verification mail from the embedded inbox.

## Scope decisions (from brainstorming)

- **Single self-contained `index.html`** — HTML + inline `<style>` + inline `<script>`. No external CDN; uses the native `fetch` API (not axios).
- **Trigger:** explicit **Generate** button (not auto-on-load), so the user controls when accounts are created and can regenerate.
- **Inbox reading:** YES — log into mail.tm (JWT) and poll `/messages` to display incoming mail, highlighting the Reddit verification message.

## Architecture (high-level)

```
[Generate] ──► createMailAccount()  (POST /accounts on mail.tm)
           ──► loginMailAccount()   (POST /token → JWT)
           ──► genRedditCreds()     (username + strong password, local)
           ──► render credentials
           ──► startInboxPolling()  (GET /messages every ~5s with JWT)
[Open Reddit]  ──► new tab to https://www.reddit.com/register
[Download .md] ──► Blob → <a download="<username>.md">
```

All state (JWT, credentials, polling timer) lives in memory only — nothing persisted to `localStorage`.

## Components (modules inside the one file)

| Module     | Responsibility                                                        | Depends on              |
|------------|-----------------------------------------------------------------------|-------------------------|
| `mailtm`   | `getDomain()`, `createAccount()`, `login()`, `getMessages()`, `getMessage(id)` | mail.tm API via `fetch` |
| `creds`    | `strongPassword(len)`, `redditUsername()`                             | `crypto.getRandomValues` |
| `ui`       | render credential cards, copy-to-clipboard, polling loop, error banner | DOM                     |
| `download` | build markdown string and trigger `<username>.md` download            | Blob / URL APIs         |

Each module is a small, independently understandable unit communicating through plain function calls and return values.

## mail.tm API usage

Base URL: `https://api.mail.tm`

- `GET /domains?page=1` → pick first active domain (`hydra:member[0].domain`).
- `POST /accounts` with `{ address, password }` → `201 Created` on success.
- `POST /token` with `{ address, password }` → `{ token }` (JWT).
- `GET /messages?page=1` with `Authorization: Bearer <token>` → list of messages.
- `GET /messages/{id}` with bearer token → full message (to read body / verification link).

The API supports CORS, so calls work directly from a static page (the existing
`TempMailGenerator` already does this).

## Credential generation (chosen approach)

- **Passwords (both mail.tm and Reddit):** generated with `crypto.getRandomValues`
  (cryptographically secure). Length **16**, guaranteed to include at least one
  lowercase, one uppercase, one digit, and one symbol from a Reddit-safe set
  `!@#$%^&*-_=+`. Character positions shuffled so the guaranteed chars are not
  always at the front.
- **Reddit username:** readable combo `Word + Word + 3-digit number`
  (e.g. `SwiftPanda742`) from a small embedded wordlist. Stays within Reddit's
  limits (3–20 chars, `A-Za-z0-9_-`) and looks more human than pure random.
- **mail.tm password ≠ Reddit password** — two distinct accounts, two distinct
  strong passwords.

## Data flow & error handling

- Every API call wrapped in `try/catch`; failures show a visible error banner
  (no silent failures).
- mail.tm rate limit (~8 QPS): inbox polling runs every **5s**; stops after a
  reasonable idle window or on repeated errors, and can be stopped manually
  via a **Stop** button.
- If `POST /accounts` returns `422` (e.g. address taken / domain busy), retry
  once with a freshly randomized address.
- JWT stored only in a JS variable for the page lifetime.

## UI layout

- Header with **Generate** (and **Regenerate** after first use).
- Two credential cards: **mail.tm** (email + password) and **Reddit**
  (username + password). Each value has a copy-to-clipboard button.
- Action row: **Open Reddit register** (new tab) and **Download .md**.
- **Inbox** panel below: live list of messages; the Reddit verification message
  is visually highlighted, and clicking it shows the body / verification link.
- Error banner area, hidden until needed.

## The downloaded `.md` file

- **Filename:** `<reddit_username>.md`
- **Contents:**

```markdown
# Reddit Account — <reddit_username>

Created: <ISO timestamp>

## mail.tm (temporary email)
- Email: <address>
- Password: <mailtm_password>

## Reddit
- Username: <reddit_username>
- Password: <reddit_password>
- Register: https://www.reddit.com/register
```

## Out of scope (YAGNI)

- No automatic submission of the Reddit registration form.
- No persistence/history of generated accounts.
- No multi-domain selection UI (first active domain is used).
- No backend — purely client-side static file.

## Success criteria

- Opening `index.html` and clicking **Generate** produces a working mail.tm
  account and valid-looking Reddit credentials within a few seconds.
- Both passwords meet "strong" criteria (length 16, mixed character classes).
- The inbox panel shows messages arriving at the mail.tm address (verifiable by
  sending the address a test email or triggering Reddit's verification mail).
- The **Download** button saves a `.md` file whose name is the Reddit username
  and whose contents match the displayed credentials.
- The **Open Reddit** button navigates to `https://www.reddit.com/register`.
```

