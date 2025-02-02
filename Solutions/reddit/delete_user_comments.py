import requests
from requests.auth import HTTPBasicAuth

# USE THIS SCRIPT TO DELETE ALL OF YOUR COMMENTS USING THE OFFICIAL REDDIT API

def get_access_token(client_id, client_secret, username, password):
    """
    Obtain an access token using Reddit's API.
    """
    auth = HTTPBasicAuth(client_id, client_secret)
    data = {
        'grant_type': 'password',
        'username': username,
        'password': password
    }
    headers = {'User-Agent': 'DeleteRedditComments/0.1 by YOUR_USERNAME'}

    response = requests.post('https://www.reddit.com/api/v1/access_token', 
                             auth=auth, data=data, headers=headers)

    if response.status_code == 200:
        return response.json().get('access_token')
    else:
        raise Exception(f"Failed to get access token: {response.status_code} {response.text}")


def get_comments(access_token, username, limit = 100):
    """
    Fetch all comments for the authenticated user.
    """
    headers = {
        'Authorization': f'bearer {access_token}',
        'User-Agent': 'DeleteRedditComments/0.1 by YOUR_USERNAME'
    }

    url = f'https://oauth.reddit.com/user/{username}/comments'
    comments = []

    params = {'limit': limit}
    response = requests.get(url, headers=headers, params=params)
    if response.status_code != 200:
        raise Exception(f"Failed to fetch comments: {response.status_code} {response.text}")
    data = response.json()
    comments.extend(data.get('data', {}).get('children', []))
    return comments


def delete_comment(access_token, comment_id):
    """
    Delete a specific comment by its ID.
    """
    headers = {
        'Authorization': f'bearer {access_token}',
        'User-Agent': 'DeleteRedditComments/0.1 by YOUR_USERNAME'
    }

    url = f'https://oauth.reddit.com/api/del?id=t1_{comment_id}'
    response = requests.post(url, headers=headers)

    if response.status_code == 200:
        print(f"Deleted comment ID: {comment_id}")
    else:
        print(f"Failed to delete comment {comment_id}: {response.status_code} {response.text}")


def delete_all_comments(client_id, client_secret, username, password):
    """
    Deletes all comments from the authenticated Reddit account.
    """
    try:
        access_token = get_access_token(client_id, client_secret, username, password)

        continue_process = True
        while continue_process:
            comments = get_comments(access_token, username)
            print(len(comments))
            if len(comments) == 0:
                continue_process = False
            for comment in comments:
                comment_id = comment['data']['id']
                delete_comment(access_token, comment_id)

        print("All comments have been deleted.")

    except Exception as e:
        print(f"An error occurred: {e}")

# HOWTO: Get client_id and client_secret
# Create application for free at: https://www.reddit.com/prefs/apps
# Tutorial here: How to Get Your API Keys in 2024 https://youtu.be/0mGpBxuYmpU
# Tip: After finish this process you can delete the application
if __name__ == "__main__":
    delete_all_comments(
        client_id="YOUR_REDDIT_CLIENT_ID",  # Replace with your Reddit app client ID
        client_secret="YOUR_REDDIT_CLIENT_SECRET",  # Replace with your Reddit app client secret
        username="YOUR_REDDIT_USERNAME",  # Replace with your Reddit username
        password="YOUR_REDDIT_PASSWORD"  # Replace with your Reddit password
    )