import { GoogleTagService } from "../services/GoogleTagService";

export default function TestsPage() {
    return (
        <div>
            <h1 className="text-3xl font-bold mb-6 text-center">Tests page</h1>
            <div>
                <h3>Google tag event</h3>
                <div className="text-center">

                    <button onClick={() => {
                        const googleEvent = GoogleTagService.CreateEvent('basket', {
                            id: 3745092,
                            item: 'mens grey t-shirt',
                            description: ['round neck', 'long sleeved'],
                            size: 'L',
                        });

                        GoogleTagService.LogEvent(googleEvent);
                    }} className="bg-green-500 hover:bg-green-700 text-white font-bold py-1 px-3 rounded">trigger</button>
                </div>
            </div>
        </div>
    )
}