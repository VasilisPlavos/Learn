import { useState } from "react";
import { TMDB } from "../services/tmdbApi";

export default function LoginPage() {
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState(null);

    const handleLogin = async () => {
        setIsLoading(true);
        setError(null);
        try {
            const { request_token } = await TMDB.createRequestToken();

            // Construct the redirect URL. Make sure this matches the callback URL in your TMDB API settings if required.
            // For development, localhost is usually fine. For production, use your deployed app's URL.
            // Redirect the user to TMDB for authentication
            const redirectUrl = `${window.location.origin}/auth/approved`; // Use current origin + callback path
            window.location.href = `https://www.themoviedb.org/authenticate/${request_token}?redirect_to=${redirectUrl}`
        } catch (err) {
            console.error("Login failed:", err);
            // setError(`Failed to initiate login: ${err.message || 'Unknown error'}`);
            setIsLoading(false);
        }
        // No need to set isLoading back to false if redirect happens
    };


    return (
        <div>
            <h1 className="text-2xl font-bold mb-4">Login with TMDB</h1>
            <button
                onClick={handleLogin}
                className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
            >
                {isLoading ? 'Redirecting...' : 'Login with TMDB'}
            </button>
            {error && <p className="text-red-500 mt-2">Error: {error}</p>}
            {!isLoading && (
                <p className="mt-4 text-sm text-gray-600 dark:text-gray-400">
                    You will be redirected to The Movie Database to approve access.
                </p>
            )}
        </div>
    );
}