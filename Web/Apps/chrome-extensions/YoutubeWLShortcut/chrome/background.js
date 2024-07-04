function executeYouTubeCommand(action) {
  const getAddVideoParams = (videoId) => ({
    clickTrackingParams: "",
    commandMetadata: { webCommandMetadata: { sendPost: true, apiUrl: "/youtubei/v1/browse/edit_playlist" } },
    playlistEditEndpoint: { playlistId: "WL", actions: [{ addedVideoId: videoId, action: "ACTION_ADD_VIDEO" }] }
  });
  
  const getRemoveVideoParams = (videoId) => ({
    clickTrackingParams: "",
    commandMetadata: { webCommandMetadata: { sendPost: true, apiUrl: "/youtubei/v1/browse/edit_playlist" } },
    playlistEditEndpoint: { playlistId: "WL", actions: [{ action: "ACTION_REMOVE_VIDEO_BY_VIDEO_ID", removedVideoId: videoId }] }
  });

  const sendActionToNativeYouTubeHandler = (getParams) => {
    const videoId = new URL(window.location.href).searchParams.get("v");
    const appElement = document.querySelector("ytd-app");

    if (!videoId || !appElement) {
      return;
    }
  
    const event = new window.CustomEvent('yt-action', {
      detail: {
        actionName: 'yt-service-request',
        returnValue: [],
        args: [{ data: {} }, getParams(videoId)],
        optionalAction: false,
      }
    });
  
    appElement.dispatchEvent(event);
  };

  try {
    if (action === "add") {
      sendActionToNativeYouTubeHandler(getAddVideoParams);
    }
    if (action === "remove") {
      sendActionToNativeYouTubeHandler(getRemoveVideoParams);
    }
  } catch (error) {
    console.warn("Error while sending message to native YouTube handler", error);
  }
}

function initAction(currentUrl) {
  if (lastUrl === "" || lastUrl !== currentUrl) {
    lastUrl = currentUrl;
    action = "add";
    return;
  }

  action = action === "add" ? "remove" : "add";
  lastUrl = currentUrl;
}

var action = "";
var lastUrl = "";
chrome.commands.onCommand.addListener(async (command) => {

  if (command === "add-remove-watch-later") {

    const [activeYouTubeTab] = await chrome.tabs.query({
      active: true,
      lastFocusedWindow: true,
      url: 'https://www.youtube.com/*',
    });

    if (!activeYouTubeTab) return;

    initAction(activeYouTubeTab.url);
    await chrome.scripting.executeScript({
      target: {
        tabId: activeYouTubeTab.id,
      },
      func: executeYouTubeCommand,
      args: [action],
    });
  }
});
