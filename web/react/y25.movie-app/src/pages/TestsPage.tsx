import { GoogleTagEvents } from "../models/googletag";

export default function TestsPage() {
    return (
        <div>
            <h1 className="text-3xl font-bold mb-6 text-center">Tests page</h1>
            <div>
                <h3>Google tag event</h3>
                <div className="text-center">

                    <button onClick={() => {

                        const googleEvent = GoogleTagEvents.NewsAction(
                            '1',
                            'rewrite_pressed',
                        );

                        window.gtag(
                            'event',
                            googleEvent.eventName,
                            googleEvent.params,
                        );


                    }} className="bg-green-500 hover:bg-green-700 text-white font-bold py-1 px-3 rounded">trigger</button>
                </div>
            </div>
        </div>
    )
}