---
name: chuck
description: Trying to find a joke that Vasilis likes.
---

# Chuck norris fav joke

## Overview

Fetch a Chuck Norris joke to make Vasilis laugh.

## When to Use

- Vasilis asks for it

## Usage

Simply invoke `/chuck` and share the joke.

Optionally specify a category:
- `dev` - Developer jokes (great for tech teams)
- `movie` - Movie references
- `sport` - Sports jokes

## Implementation

1. **Read preferences** - Load `preferences.json` to understand Vasilis's taste

2. **Fetch and Score Loop** (max 5 attempts):
   - Fetch a joke from the API (prioritize favorite categories if any)
   - **ALWAYS print your thinking** about the joke:
     - Compare to liked jokes: themes, style, humor type
     - Compare to disliked jokes: avoid similar patterns
     - Consider: Is it clever? Has wordplay? Is it too generic?
   - Score the joke (1-10) based on analysis
   - If score < 7, reject and fetch another joke
   - If score >= 7, present it to Vasilis

3. **Scoring Criteria** (analyze and print reasoning for each):
   - **Cleverness**: Wordplay, unexpected twists, smart references (+points)
   - **Relevance**: Tech/dev jokes if those are in favorites (+points)
   - **Originality**: Not a generic "Chuck Norris is strong" joke (+points)
   - **Similar to liked**: Matches themes/style of liked jokes (+points)
   - **Similar to disliked**: Matches patterns of disliked jokes (-points)

4. **Present the winning joke** and ask for feedback

5. **Update preferences** based on response:
   - **Liked**: Add to `liked[]`, increment `likedCount`, update `favoriteCategories`
   - **Disliked**: Add to `disliked[]`, increment `dislikedCount`
   - Always increment `totalJokes`

## Feedback Storage

The `preferences.json` file tracks:
- `liked[]` - Jokes that made Vasilis laugh (learn from these patterns)
- `disliked[]` - Jokes that didn't land (avoid these patterns)
- `favoriteCategories[]` - Categories with best hit rate
- `stats` - Overall statistics

## Example Output

```
🤔 **Evaluating jokes...**

Joke 1: "Chuck Norris counted to infinity. Twice."
- Thinking: Generic "impossible feat" joke, similar to disliked patterns
- Score: 4/10 - REJECTED

Joke 2: "Chuck Norris can unit test entire applications with a single assert."
- Thinking: Dev humor, clever programming reference, not in disliked list
- Score: 8/10 - APPROVED!

**Here's the joke:**

> "Chuck Norris can unit test entire applications with a single assert."

Did you like it? 👍 or 👎
```

## Quick Reference

| Endpoint | Purpose |
|----------|---------|
| `/jokes/random` | Random joke |
| `/jokes/random?category=dev` | Dev-specific joke |
| `/jokes/categories` | List all categories |

## Available Categories

dev, movie, sport, celebrity, fashion, food, history, money, music, political, religion, science, travel, animal, career, explicit
