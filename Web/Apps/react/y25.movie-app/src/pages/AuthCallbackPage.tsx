import { useEffect, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { TMDB } from "../services/tmdbApi";

export default function AuthCallbackPage() {
    const location = useLocation();
    const navigate = useNavigate();
    const [status, setStatus] = useState('Authenticating...');

    useEffect(() => {
        const params = new URLSearchParams(location.search);
        const requestToken = params.get('request_token');
        const approved = params.get('approved');

        if (approved === 'true' && requestToken) {
            initUserAsync(requestToken)
        };
    })

    async function initUserAsync(requestToken: string) {
        const response = await TMDB.createSession(requestToken);
        const sessionId = response.data.session_id;
        if (sessionId) {
            const account = await TMDB.getAccountDetails(sessionId);
            const user = {
                sessionId: sessionId,
                accountId: account.id,
                userName: account.username
            };
            localStorage.setItem('user', JSON.stringify(user));
            setStatus('Authentication successful! Redirecting...');
            navigate('/');
        }
    };



    return (
        <div>
            <h1 className="text-2xl font-bold mb-4">TMDB Authentication</h1>
            <p>{status}</p>
        </div>
    );
}


