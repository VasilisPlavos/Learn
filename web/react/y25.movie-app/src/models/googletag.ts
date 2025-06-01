declare global {
  interface Window {
    gtag: (...args: any[]) => void;
  }
}

type EventParams = Record<string, any>;
export interface trackerObject {
  eventName: string;
  urlPathname?: string;
  params?: EventParams;
}

export const GoogleTagEvents = {
  CreateEvent(eventName: string, params: EventParams): trackerObject {
    return { eventName, params };
  },

  PageView(pagePathName: string): trackerObject {
    return {
      urlPathname: pagePathName,
      eventName: 'page_view',
    };
  },

  // Like news
  // Rewrite news
  NewsAction(
    newsId: string,
    action: 'like_pressed' | 'rewrite_pressed',
  ): trackerObject {
    return {
      eventName: 'news_action',
      params: { newsId, action },
    };
  }
};
