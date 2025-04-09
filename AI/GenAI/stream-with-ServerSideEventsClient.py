import requests
import json
import sseclient

# https://platform.openai.com/settings/organization/api-keys
API_KEY = 'sk-proj-KEYHERE'

# https://youtu.be/x8uwwLNxqis
def performRequest():
    url = 'https://api.openai.com/v1/completions'
    headers = {
        'Accept': 'text/event-stream',
        'Authorization': 'Bearer ' + API_KEY
    }

    body = {
        "model": "gpt-3.5-turbo-instruct",
        "prompt": "What is python?",
        "max_tokens": 100,
        "temperature": 0,
        "stream": True
    }

    response = requests.post(url, headers=headers, json=body, stream=True)
    client = sseclient.SSEClient(response)
    for event in client.events():        
        if event.data != '[DONE]':
            print(json.loads(event.data)["choices"][0]["text"], end="", flush=True)


if __name__ == '__main__':
    performRequest()
