const domain = 'meet.jit.si'
const options = {
    roomName: 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9',
    width: 700,
    height: 700,
    parentNode: document.querySelector('#jitsi'),
    interfaceConfigOverwrite: { 
        DEFAULT_REMOTE_DISPLAY_NAME: 'remote_display',
        DEFAULT_LOCAL_DISPLAY_NAME: 'local_display',
        JITSI_WATERMARK_LINK: 'https://google.com',
        SHOW_JITSI_WATERMARK: false,
        SHOW_BRAND_WATERMARK: true,
        BRAND_WATERMARK_LINK: 'https://www.gov.gr/gov_logos-08_16x16_favicon.ico',
        PROVIDER_NAME: 'provider_name',
        TOOLBAR_BUTTONS: [
            'microphone', 'camera', 'closedcaptions'
        ]

     },
    
}

const api = new JitsiMeetExternalAPI(domain, options)

// var jitsiId = document.getElementById(jitsi)
