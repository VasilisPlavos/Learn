// https://developers.google.com/tag-platform/gtagjs/reference/events

declare global {
    interface Window {
        gtag: {
            // (command: 'config', targetId: string, config?: EventParams): void;
            (command: 'event', eventName: string, eventParams?: EventParams): void;
            // (command: string, ...args: any[]): void;
        };
    }
}

type EventParams = Record<string, any>;
export interface GoogleTagEvent {
    eventName: string;
    params?: EventParams;
}

export const GoogleTagService = {
    CreateEvent(eventName: string, params: EventParams): GoogleTagEvent {
        return { eventName, params };
    },

    // Like news
    // Rewrite news
    CreateNewsActionEvent(
        newsId: string,
        action: 'like_pressed' | 'rewrite_pressed',
    ): GoogleTagEvent {
        return {
            eventName: 'news_action',
            params: { newsId, action },
        };
    },

    LogEvent(googleEvent: GoogleTagEvent): void {
        window.gtag(
            'event',
            googleEvent.eventName,
            googleEvent.params,
        );
    },
}