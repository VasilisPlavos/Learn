const domain = 'meet.jit.si'
const options = {
    roomName: 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9',
    width: 700,
    height: 700,
    parentNode: document.querySelector('#jitsi'),
    interfaceConfigOverwrite: { 
        DISABLE_VIDEO_BACKGROUND: true,
        DEFAULT_REMOTE_DISPLAY_NAME: 'remote_display',
        DEFAULT_LOCAL_DISPLAY_NAME: 'local_display',
        JITSI_WATERMARK_LINK: 'https://google.com',
        SHOW_JITSI_WATERMARK: false,
        SHOW_BRAND_WATERMARK: true,
        BRAND_WATERMARK_LINK: 'https://www.gov.gr/gov_logos-08_16x16_favicon.ico',
        PROVIDER_NAME: 'provider_name',
     },
    
}

const api = new JitsiMeetExternalAPI(domain, options)
api.executeCommand('displayName', 'New Nickname');
api.getNumberOfParticipants()

setTimeout(function(){

    // api.executeCommand('password', '12345');
    api.executeCommand('toggleChat', 'toggle-chat');

    // api.executeCommand('toggleVideo', 'toggle-video');
    // api.executeCommand('toggleTileView', 'toggle-tile-viewt');

    
    return;
}, 1000);



api.isVideoAvailable().then(available => {
    api.executeCommand('password', '12345');
});

api.executeCommand('subject', 'subject');
// api.executeCommand('toggleAudio', 'toggle-audio');
console.log(api._participants)


function text_listener(data){
    console.log(JSON.stringify(data))
    }


api.addEventListener('outgoingMessage', text_listener);

api.executeCommand('email', 'em@amil.fe')

api.dispose();