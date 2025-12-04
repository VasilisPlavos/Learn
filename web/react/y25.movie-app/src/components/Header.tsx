import { Link, useNavigate } from "react-router-dom";
import { TMDB } from "../services/tmdbApi";

export default function Header() {
    const user = TMDB.getUser();
    const navigate = useNavigate();

    const handleLogout = () => {
        TMDB.logout();
        navigate('/');
    };

    return (
        <header className="bg-blue-600 dark:bg-blue-800 text-white p-4 shadow-md">
            <nav className="container mx-auto flex justify-between items-center">
                <Link to="/" className="text-xl font-bold hover:text-blue-200">
                    TMDB Movies
                </Link>
                <ul className="flex space-x-4 items-center">
                    <li>
                        <Link to="/tests" className="hover:text-blue-200">Tests</Link>
                    </li>
                    <li>
                        <Link to="/" className="hover:text-blue-200">Home</Link>
                    </li>
                    {user?.sessionId ? (
                        <>
                            <li>
                                <Link to="/favorites" className="hover:text-blue-200">Favorites</Link>
                            </li>
                            <li className="flex items-center space-x-2">
                                <span className="text-sm">Welcome {user.userName}</span>
                                {/* You might want to display user avatar if available: user.avatar.tmdb.avatar_path */}
                                <button
                                    onClick={handleLogout}
                                    className="bg-red-500 hover:bg-red-700 text-white text-sm font-bold py-1 px-3 rounded"
                                >
                                    Logout
                                </button>
                            </li>
                        </>
                    ) : (
                        <li>
                            <Link
                                to="/login"
                                className="bg-green-500 hover:bg-green-700 text-white font-bold py-1 px-3 rounded"
                            >
                                Login
                            </Link>
                        </li>
                    )}
                </ul>
            </nav>

        </header>
    );
}