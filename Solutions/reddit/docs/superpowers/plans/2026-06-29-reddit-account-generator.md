# Reddit Account Generator Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** A single self-contained static HTML file that generates a mail.tm temporary email + strong Reddit credentials, polls the mail.tm inbox, links to Reddit registration, and downloads a `<username>.md` summary.

**Architecture:** One `index.html` with inline `<style>` and `<script>`. Three pure-ish modules as IIFEs (`creds`, `download`, `mailtm`) plus a thin UI layer wiring DOM events. Pure logic (password/username generation, markdown building) is verified by an in-page self-test harness reachable at `index.html#test`. API, DOM, clipboard, and polling are verified manually in the browser.

**Tech Stack:** Vanilla HTML/CSS/JS, native `fetch`, `crypto.getRandomValues`, Blob/URL download. No frameworks, no CDN.

## Global Constraints

- Single self-contained file: `Solutions/reddit/index.html` — all HTML + CSS + JS inline. No external CDN, no axios. Use native `fetch`.
- Passwords generated with `crypto.getRandomValues` only (never `Math.random`). Length **16**, at least one lowercase, uppercase, digit, and symbol from the set `!@#$%^&*-_=+`.
- Reddit username matches `^[A-Za-z0-9_-]{3,20}$` (Reddit's limits).
- mail.tm password and Reddit password are two distinct strong passwords.
- mail.tm base URL: `https://api.mail.tm`. Reddit register URL: `https://www.reddit.com/register`.
- Inbox polling interval: 5000 ms. Polling can be stopped via a Stop button.
- No persistence: JWT and credentials live only in JS memory (no `localStorage`).
- Downloaded file name is exactly `<reddit_username>.md`.

---

### Task 1: HTML scaffold, styles, and self-test harness

**Files:**
- Create: `Solutions/reddit/index.html`

**Interfaces:**
- Consumes: nothing.
- Produces: DOM element ids used by later tasks — `#generate`, `#stop`, `#error`, `#mail-email`, `#mail-password`, `#reddit-username`, `#reddit-password`, `#open-reddit`, `#download`, `#inbox`, `#message-view`, `#test-results`. Global helpers `setError(msg)`, `setBusy(bool)`, `renderTestResults(results)`, and an empty `runSelfTests()` that the `#test` hash invokes. A module-level `state` object: `{ token, account, pollTimer, seen }`.

- [ ] **Step 1: Create the file with full scaffold**

Create `Solutions/reddit/index.html`:

```html
<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1.0" />
  <title>Reddit Account Generator</title>
  <style>
    :root { --bg:#0f1115; --card:#1a1d24; --accent:#ff4500; --text:#e6e6e6; --muted:#9aa0aa; --ok:#3fb950; --err:#f85149; }
    * { box-sizing: border-box; }
    body { margin:0; font-family: system-ui, sans-serif; background:var(--bg); color:var(--text); padding:24px; }
    main { max-width:720px; margin:0 auto; }
    h1 { font-size:1.4rem; }
    .row { display:flex; gap:12px; flex-wrap:wrap; align-items:center; margin:16px 0; }
    button { background:var(--accent); color:#fff; border:0; border-radius:8px; padding:10px 16px; font-size:0.95rem; cursor:pointer; }
    button.secondary { background:#2d323c; }
    button:disabled { opacity:0.5; cursor:not-allowed; }
    a.btn { display:inline-block; text-decoration:none; background:var(--accent); color:#fff; border-radius:8px; padding:10px 16px; font-size:0.95rem; cursor:pointer; }
    .card { background:var(--card); border-radius:12px; padding:16px; margin:12px 0; }
    .card h2 { font-size:1rem; margin:0 0 12px; color:var(--muted); }
    .field { display:flex; align-items:center; gap:8px; margin:8px 0; }
    .field label { width:90px; color:var(--muted); font-size:0.85rem; }
    .field input { flex:1; background:#0f1115; color:var(--text); border:1px solid #2d323c; border-radius:6px; padding:8px; font-family:monospace; }
    .copy { padding:6px 10px; font-size:0.8rem; background:#2d323c; }
    #error { background:#3a1d1d; color:var(--err); border-radius:8px; padding:12px; margin:12px 0; display:none; }
    #inbox { list-style:none; padding:0; margin:0; }
    #inbox li { background:#0f1115; border:1px solid #2d323c; border-radius:8px; padding:10px; margin:6px 0; cursor:pointer; }
    #inbox li.reddit { border-color:var(--accent); box-shadow:0 0 0 1px var(--accent); }
    #inbox li .from { color:var(--muted); font-size:0.8rem; }
    #message-view { background:#0f1115; border:1px solid #2d323c; border-radius:8px; padding:12px; margin-top:8px; white-space:pre-wrap; word-break:break-word; display:none; }
    #test-results { font-family:monospace; }
    #test-results .pass { color:var(--ok); }
    #test-results .fail { color:var(--err); }
    .muted { color:var(--muted); font-size:0.85rem; }
  </style>
</head>
<body>
  <main>
    <h1>Reddit Account Generator</h1>
    <p class="muted">Creates a temporary mail.tm inbox and ready-to-use Reddit credentials.</p>

    <div class="row">
      <button id="generate">Generate</button>
      <button id="stop" class="secondary" style="display:none;">Stop inbox polling</button>
    </div>

    <div id="error"></div>

    <section id="results" style="display:none;">
      <div class="card">
        <h2>mail.tm (temporary email)</h2>
        <div class="field"><label>Email</label><input id="mail-email" readonly /><button class="copy" data-target="mail-email">Copy</button></div>
        <div class="field"><label>Password</label><input id="mail-password" readonly /><button class="copy" data-target="mail-password">Copy</button></div>
      </div>

      <div class="card">
        <h2>Reddit</h2>
        <div class="field"><label>Username</label><input id="reddit-username" readonly /><button class="copy" data-target="reddit-username">Copy</button></div>
        <div class="field"><label>Password</label><input id="reddit-password" readonly /><button class="copy" data-target="reddit-password">Copy</button></div>
        <div class="row">
          <a id="open-reddit" class="btn" href="https://www.reddit.com/register" target="_blank" rel="noopener">Open Reddit register</a>
          <button id="download" class="secondary">Download .md</button>
        </div>
      </div>

      <div class="card">
        <h2>Inbox</h2>
        <ul id="inbox"></ul>
        <p id="inbox-empty" class="muted">Waiting for messages…</p>
        <div id="message-view"></div>
      </div>
    </section>

    <div id="test-results"></div>
  </main>

  <script>
    "use strict";

    const state = { token: null, account: null, pollTimer: null, seen: new Set() };

    const els = {
      generate: document.querySelector("#generate"),
      stop: document.querySelector("#stop"),
      error: document.querySelector("#error"),
      results: document.querySelector("#results"),
      mailEmail: document.querySelector("#mail-email"),
      mailPassword: document.querySelector("#mail-password"),
      redditUsername: document.querySelector("#reddit-username"),
      redditPassword: document.querySelector("#reddit-password"),
      openReddit: document.querySelector("#open-reddit"),
      download: document.querySelector("#download"),
      inbox: document.querySelector("#inbox"),
      inboxEmpty: document.querySelector("#inbox-empty"),
      messageView: document.querySelector("#message-view"),
      testResults: document.querySelector("#test-results"),
    };

    function setError(msg) {
      if (!msg) { els.error.style.display = "none"; els.error.textContent = ""; return; }
      els.error.style.display = "block";
      els.error.textContent = msg;
    }

    function setBusy(busy) {
      els.generate.disabled = busy;
      els.generate.textContent = busy ? "Working…" : "Generate";
    }

    // === Self-test harness (open index.html#test) ===
    function renderTestResults(results) {
      const passed = results.filter(r => r.pass).length;
      const lines = results.map(r =>
        `<div class="${r.pass ? "pass" : "fail"}">${r.pass ? "PASS" : "FAIL"} — ${r.name}${r.error ? " :: " + r.error : ""}</div>`
      ).join("");
      els.testResults.innerHTML =
        `<h2>Self-tests: ${passed}/${results.length} passed</h2>` + lines;
    }

    function runSelfTests() {
      const results = [];
      const test = (name, fn) => {
        try { fn(); results.push({ name, pass: true }); }
        catch (e) { results.push({ name, pass: false, error: e.message }); }
      };
      const assert = (cond, msg) => { if (!cond) throw new Error(msg || "assertion failed"); };

      // creds tests added in Task 2
      // markdown tests added in Task 3

      renderTestResults(results);
      // eslint-disable-next-line no-console
      console.table(results);
    }

    if (location.hash === "#test") runSelfTests();
  </script>
</body>
</html>
```

- [ ] **Step 2: Verify the page renders**

Open `Solutions/reddit/index.html` in a browser.
Expected: Title "Reddit Account Generator", a **Generate** button, no visible error, no results cards (they are `display:none`), empty test-results area.

- [ ] **Step 3: Verify the self-test hook runs**

Open `Solutions/reddit/index.html#test` in a browser.
Expected: A line "Self-tests: 0/0 passed" appears (the harness runs with no tests yet). No JS console errors.

- [ ] **Step 4: Commit**

```bash
git add index.html
git commit -m "feat(reddit-gen): scaffold HTML, styles, and self-test harness"
```

---

### Task 2: Credential generation module (`creds`)

**Files:**
- Modify: `Solutions/reddit/index.html` (add `creds` IIFE and its self-tests inside `<script>`)

**Interfaces:**
- Consumes: nothing.
- Produces: `creds.strongPassword(len = 16) -> string`, `creds.redditUsername() -> string`, `creds.randomString(len, alphabet) -> string`. Used by Task 4 (`randomString` for the email local part) and Task 5 (`strongPassword`, `redditUsername`).

- [ ] **Step 1: Write the failing self-tests**

In `index.html`, inside `runSelfTests()`, replace the line `// creds tests added in Task 2` with:

```js
      test("strongPassword length is 16", () => {
        assert(creds.strongPassword(16).length === 16, "wrong length");
      });
      test("strongPassword has all char classes", () => {
        const p = creds.strongPassword(16);
        assert(/[a-z]/.test(p), "no lowercase");
        assert(/[A-Z]/.test(p), "no uppercase");
        assert(/[0-9]/.test(p), "no digit");
        assert(/[!@#$%^&*\-_=+]/.test(p), "no symbol");
      });
      test("redditUsername matches Reddit constraints", () => {
        const u = creds.redditUsername();
        assert(/^[A-Za-z0-9_-]{3,20}$/.test(u), "invalid username: " + u);
      });
      test("randomString length and alphabet", () => {
        const s = creds.randomString(12, "abc");
        assert(s.length === 12, "wrong length");
        assert(/^[abc]{12}$/.test(s), "out-of-alphabet chars: " + s);
      });
```

- [ ] **Step 2: Run the tests to verify they fail**

Open `Solutions/reddit/index.html#test`.
Expected: 4 FAIL lines, each error mentioning `creds is not defined` (the module does not exist yet). "Self-tests: 0/4 passed".

- [ ] **Step 3: Implement the `creds` module**

In `index.html`, add this IIFE immediately after the `const state = ...` line:

```js
    const creds = (() => {
      const LOWER = "abcdefghijklmnopqrstuvwxyz";
      const UPPER = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
      const DIGITS = "0123456789";
      const SYMBOLS = "!@#$%^&*-_=+";
      const ALL = LOWER + UPPER + DIGITS + SYMBOLS;

      // Unbiased random integer in [0, max) using rejection sampling.
      function randInt(max) {
        const arr = new Uint32Array(1);
        const limit = Math.floor(0x100000000 / max) * max;
        let x;
        do { crypto.getRandomValues(arr); x = arr[0]; } while (x >= limit);
        return x % max;
      }

      function pick(chars) { return chars[randInt(chars.length)]; }

      function shuffle(arr) {
        for (let i = arr.length - 1; i > 0; i--) {
          const j = randInt(i + 1);
          [arr[i], arr[j]] = [arr[j], arr[i]];
        }
        return arr;
      }

      function randomString(len, alphabet) {
        let s = "";
        for (let i = 0; i < len; i++) s += alphabet[randInt(alphabet.length)];
        return s;
      }

      function strongPassword(len = 16) {
        if (len < 4) throw new Error("password length must be >= 4");
        const chars = [pick(LOWER), pick(UPPER), pick(DIGITS), pick(SYMBOLS)];
        while (chars.length < len) chars.push(pick(ALL));
        return shuffle(chars).join("");
      }

      const ADJ = ["Swift","Brave","Quiet","Lucky","Cosmic","Silent","Golden","Rapid","Mighty","Clever","Frosty","Sunny","Wild","Noble","Vivid"];
      const NOUN = ["Panda","Falcon","Otter","Tiger","Comet","Maple","Raven","Wolf","Lynx","Heron","Bison","Koala","Cobra","Ember","Drift"];

      function redditUsername() {
        const a = ADJ[randInt(ADJ.length)];
        const n = NOUN[randInt(NOUN.length)];
        const num = String(randInt(900) + 100); // 100..999
        return `${a}${n}${num}`;
      }

      return { strongPassword, redditUsername, randomString };
    })();
```

- [ ] **Step 4: Run the tests to verify they pass**

Open `Solutions/reddit/index.html#test`.
Expected: "Self-tests: 4/4 passed", all lines green PASS.

- [ ] **Step 5: Commit**

```bash
git add index.html
git commit -m "feat(reddit-gen): add crypto-secure password and username generation"
```

---

### Task 3: Markdown builder + download (`download`)

**Files:**
- Modify: `Solutions/reddit/index.html` (add `download` IIFE, its self-tests, and wire the Download button)

**Interfaces:**
- Consumes: `state.account` shape `{ email, mailPassword, username, redditPassword, createdAt }` (populated in Task 5).
- Produces: `download.buildMarkdown(data) -> string`, `download.downloadMarkdown(data) -> void`. The Download button calls `downloadMarkdown(state.account)`.

- [ ] **Step 1: Write the failing self-tests**

In `runSelfTests()`, replace `// markdown tests added in Task 3` with:

```js
      test("buildMarkdown contains all fields", () => {
        const md = download.buildMarkdown({
          username: "SwiftPanda742", email: "abc@mail.tm",
          mailPassword: "Mp1!aaaa", redditPassword: "Rp2@bbbb",
          createdAt: "2026-06-29T00:00:00.000Z",
        });
        assert(md.includes("# Reddit Account — SwiftPanda742"), "missing heading");
        assert(md.includes("- Email: abc@mail.tm"), "missing email");
        assert(md.includes("- Password: Mp1!aaaa"), "missing mail password");
        assert(md.includes("- Username: SwiftPanda742"), "missing username");
        assert(md.includes("- Password: Rp2@bbbb"), "missing reddit password");
        assert(md.includes("https://www.reddit.com/register"), "missing register link");
        assert(md.includes("Created: 2026-06-29T00:00:00.000Z"), "missing timestamp");
      });
```

- [ ] **Step 2: Run the tests to verify they fail**

Open `Solutions/reddit/index.html#test`.
Expected: the new test FAILs with `download is not defined`. (creds tests still pass.)

- [ ] **Step 3: Implement the `download` module**

In `index.html`, add this IIFE immediately after the `creds` IIFE:

```js
    const download = (() => {
      function buildMarkdown(data) {
        return [
          `# Reddit Account — ${data.username}`,
          ``,
          `Created: ${data.createdAt}`,
          ``,
          `## mail.tm (temporary email)`,
          `- Email: ${data.email}`,
          `- Password: ${data.mailPassword}`,
          ``,
          `## Reddit`,
          `- Username: ${data.username}`,
          `- Password: ${data.redditPassword}`,
          `- Register: https://www.reddit.com/register`,
          ``,
        ].join("\n");
      }

      function downloadMarkdown(data) {
        const blob = new Blob([buildMarkdown(data)], { type: "text/markdown" });
        const url = URL.createObjectURL(blob);
        const a = document.createElement("a");
        a.href = url;
        a.download = `${data.username}.md`;
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);
        URL.revokeObjectURL(url);
      }

      return { buildMarkdown, downloadMarkdown };
    })();
```

- [ ] **Step 4: Wire the Download button**

In `index.html`, immediately before the `if (location.hash === "#test") runSelfTests();` line, add:

```js
    els.download.addEventListener("click", () => {
      if (!state.account) { setError("Generate an account first."); return; }
      download.downloadMarkdown(state.account);
    });
```

- [ ] **Step 5: Run the tests to verify they pass**

Open `Solutions/reddit/index.html#test`.
Expected: "Self-tests: 5/5 passed".

- [ ] **Step 6: Commit**

```bash
git add index.html
git commit -m "feat(reddit-gen): add markdown builder and .md download"
```

---

### Task 4: mail.tm API client (`mailtm`)

**Files:**
- Modify: `Solutions/reddit/index.html` (add `mailtm` IIFE)

**Interfaces:**
- Consumes: `creds.randomString` (Task 2).
- Produces:
  - `mailtm.getDomain() -> Promise<string>`
  - `mailtm.createAccount(domain, password) -> Promise<{ address }>`
  - `mailtm.login(address, password) -> Promise<string>` (JWT)
  - `mailtm.getMessages(token) -> Promise<Array>` (list, newest first per API)
  - `mailtm.getMessage(token, id) -> Promise<Object>` (full message with `text`/`html`)

- [ ] **Step 1: Implement the `mailtm` module**

In `index.html`, add this IIFE immediately after the `download` IIFE:

```js
    const mailtm = (() => {
      const BASE = "https://api.mail.tm";
      const LOCAL_ALPHABET = "abcdefghijklmnopqrstuvwxyz0123456789";

      async function getDomain() {
        const res = await fetch(`${BASE}/domains?page=1`);
        if (!res.ok) throw new Error(`getDomain failed: ${res.status}`);
        const data = await res.json();
        const members = data["hydra:member"] || [];
        const active = members.find(d => d.isActive) || members[0];
        if (!active) throw new Error("no mail.tm domain available");
        return active.domain;
      }

      async function createAccount(domain, password) {
        // Retry once on 422 (address taken / domain busy) with a fresh address.
        for (let attempt = 0; attempt < 2; attempt++) {
          const address = `${creds.randomString(16, LOCAL_ALPHABET)}@${domain}`;
          const res = await fetch(`${BASE}/accounts`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ address, password }),
          });
          if (res.status === 201) return { address };
          if (res.status === 422 && attempt === 0) continue;
          const text = await res.text();
          throw new Error(`createAccount failed: ${res.status} ${text}`);
        }
        throw new Error("createAccount failed after retry");
      }

      async function login(address, password) {
        const res = await fetch(`${BASE}/token`, {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({ address, password }),
        });
        if (!res.ok) throw new Error(`login failed: ${res.status}`);
        const data = await res.json();
        return data.token;
      }

      async function getMessages(token) {
        const res = await fetch(`${BASE}/messages?page=1`, {
          headers: { Authorization: `Bearer ${token}` },
        });
        if (!res.ok) throw new Error(`getMessages failed: ${res.status}`);
        const data = await res.json();
        return data["hydra:member"] || [];
      }

      async function getMessage(token, id) {
        const res = await fetch(`${BASE}/messages/${id}`, {
          headers: { Authorization: `Bearer ${token}` },
        });
        if (!res.ok) throw new Error(`getMessage failed: ${res.status}`);
        return res.json();
      }

      return { getDomain, createAccount, login, getMessages, getMessage };
    })();
```

- [ ] **Step 2: Verify against the live API in the browser console**

Open `Solutions/reddit/index.html`, open DevTools console, and run:

```js
await mailtm.getDomain()
```

Expected: returns a non-empty domain string (e.g. `"somedomain.com"`), no error.

Then run the full round-trip:

```js
const d = await mailtm.getDomain();
const pw = creds.strongPassword(16);
const acc = await mailtm.createAccount(d, pw);
const tok = await mailtm.login(acc.address, pw);
console.log(acc.address, tok.slice(0, 12) + "…");
(await mailtm.getMessages(tok)).length
```

Expected: logs the new address and a token prefix, and `getMessages` returns `0` (empty inbox) without error.

- [ ] **Step 3: Commit**

```bash
git add index.html
git commit -m "feat(reddit-gen): add mail.tm API client"
```

---

### Task 5: Generate flow and credential rendering

**Files:**
- Modify: `Solutions/reddit/index.html` (add generate handler, render + copy logic, button wiring)

**Interfaces:**
- Consumes: `mailtm.*` (Task 4), `creds.strongPassword`/`creds.redditUsername` (Task 2).
- Produces: `handleGenerate()` populates `state.account = { email, mailPassword, username, redditPassword, createdAt }` and `state.token`; calls `renderCredentials(account)` and `startPolling()` (defined in Task 6 — a stub is added here and replaced in Task 6).

- [ ] **Step 1: Add render, copy, and generate logic**

In `index.html`, immediately before the `els.download.addEventListener(...)` line added in Task 3, add:

```js
    function renderCredentials(acc) {
      els.mailEmail.value = acc.email;
      els.mailPassword.value = acc.mailPassword;
      els.redditUsername.value = acc.username;
      els.redditPassword.value = acc.redditPassword;
      els.results.style.display = "block";
    }

    // Temporary stub; replaced by real polling in Task 6.
    function startPolling() {}

    async function handleGenerate() {
      setError("");
      setBusy(true);
      try {
        const domain = await mailtm.getDomain();
        const mailPassword = creds.strongPassword(16);
        const { address } = await mailtm.createAccount(domain, mailPassword);
        const token = await mailtm.login(address, mailPassword);
        const username = creds.redditUsername();
        const redditPassword = creds.strongPassword(16);
        state.token = token;
        state.account = {
          email: address, mailPassword, username, redditPassword,
          createdAt: new Date().toISOString(),
        };
        state.seen = new Set();
        renderCredentials(state.account);
        startPolling();
      } catch (e) {
        setError(e.message);
      } finally {
        setBusy(false);
      }
    }

    els.generate.addEventListener("click", handleGenerate);

    // Copy-to-clipboard for any [data-target] button.
    document.querySelectorAll(".copy").forEach(btn => {
      btn.addEventListener("click", async () => {
        const el = document.getElementById(btn.dataset.target);
        try {
          await navigator.clipboard.writeText(el.value);
          const old = btn.textContent;
          btn.textContent = "Copied";
          setTimeout(() => { btn.textContent = old; }, 1200);
        } catch {
          el.select();
          setError("Clipboard blocked — value selected, copy manually.");
        }
      });
    });
```

- [ ] **Step 2: Verify the Generate flow in the browser**

Open `Solutions/reddit/index.html` and click **Generate**.
Expected:
- Button shows "Working…" then returns to "Generate".
- The mail.tm card shows an `@`-address and a 16-char password.
- The Reddit card shows a `WordWordNNN` username and a 16-char password (different from the mail password).
- No error banner.

- [ ] **Step 3: Verify copy, download, and Reddit link**

- Click **Copy** on each field → paste elsewhere → matches the shown value.
- Click **Download .md** → a file named `<reddit_username>.md` downloads; open it → contents match the four credentials, include the register link and a `Created:` timestamp.
- Click **Open Reddit register** → a new tab opens at `https://www.reddit.com/register`.

- [ ] **Step 4: Commit**

```bash
git add index.html
git commit -m "feat(reddit-gen): wire generate flow, credential rendering, copy/download"
```

---

### Task 6: Inbox polling and message view

**Files:**
- Modify: `Solutions/reddit/index.html` (replace `startPolling` stub with real polling; add inbox render, message view, Stop button)

**Interfaces:**
- Consumes: `mailtm.getMessages`/`mailtm.getMessage` (Task 4), `state.token`, `els.inbox`/`els.inboxEmpty`/`els.messageView`/`els.stop`.
- Produces: `startPolling()`, `stopPolling()`, `pollOnce()`, `renderInbox(messages)`, `showMessage(id)`. Reddit messages (sender or subject containing "reddit") get the `reddit` CSS class.

- [ ] **Step 1: Replace the polling stub with the real implementation**

In `index.html`, replace the stub line:

```js
    // Temporary stub; replaced by real polling in Task 6.
    function startPolling() {}
```

with:

```js
    function isReddit(m) {
      const from = (m.from && m.from.address ? m.from.address : "").toLowerCase();
      const subject = (m.subject || "").toLowerCase();
      return from.includes("reddit") || subject.includes("reddit");
    }

    function renderInbox(messages) {
      els.inboxEmpty.style.display = messages.length ? "none" : "block";
      els.inbox.innerHTML = "";
      for (const m of messages) {
        const li = document.createElement("li");
        if (isReddit(m)) li.classList.add("reddit");
        const from = m.from && m.from.address ? m.from.address : "(unknown)";
        li.innerHTML =
          `<div class="from">${from}</div><div>${m.subject || "(no subject)"}</div>`;
        li.addEventListener("click", () => showMessage(m.id));
        els.inbox.appendChild(li);
      }
    }

    async function showMessage(id) {
      try {
        const msg = await mailtm.getMessage(state.token, id);
        const body = msg.text || (msg.html ? msg.html.join("\n") : "") || "(empty)";
        els.messageView.style.display = "block";
        els.messageView.textContent = `Subject: ${msg.subject || ""}\n\n${body}`;
      } catch (e) {
        setError(`open message: ${e.message}`);
      }
    }

    async function pollOnce() {
      if (!state.token) return;
      try {
        const messages = await mailtm.getMessages(state.token);
        renderInbox(messages);
      } catch (e) {
        setError(`inbox: ${e.message}`);
      }
    }

    function stopPolling() {
      if (state.pollTimer) { clearInterval(state.pollTimer); state.pollTimer = null; }
      els.stop.style.display = "none";
    }

    function startPolling() {
      stopPolling();
      els.stop.style.display = "inline-block";
      pollOnce();
      state.pollTimer = setInterval(pollOnce, 5000);
    }
```

- [ ] **Step 2: Wire the Stop button**

In `index.html`, immediately after the `els.generate.addEventListener("click", handleGenerate);` line, add:

```js
    els.stop.addEventListener("click", stopPolling);
```

- [ ] **Step 3: Verify inbox polling in the browser**

Open `Solutions/reddit/index.html`, click **Generate**. The **Stop inbox polling** button appears and the inbox shows "Waiting for messages…".

Send a test email to the generated address (from any mail provider, or trigger Reddit's verification email by registering). Within ~5s the message appears in the inbox list.
Expected:
- A Reddit verification message is outlined with the accent color (the `reddit` class).
- Clicking a message shows its subject and body (including any verification link) in the message view.
- Clicking **Stop inbox polling** stops updates and hides the button.

- [ ] **Step 4: Run the self-tests once more (regression)**

Open `Solutions/reddit/index.html#test`.
Expected: "Self-tests: 5/5 passed" (pure-logic tests unaffected by the UI additions).

- [ ] **Step 5: Commit**

```bash
git add index.html
git commit -m "feat(reddit-gen): add inbox polling, Reddit highlight, message view"
```

---

## Notes for the implementer

- Open files directly via `file://` — the page needs no server. mail.tm supports CORS, so `fetch` works from `file://` and from any static host.
- If mail.tm returns `429`, you are rate-limited; wait and retry. The 5s poll interval stays under the documented ~8 QPS limit.
- Do not add a build step, bundler, or npm dependency — the deliverable is one portable `.html` file.
