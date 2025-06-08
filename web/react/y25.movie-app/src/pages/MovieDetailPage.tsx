import { useCallback, useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import { Movie } from "../models/movie.model";
import { TMDB } from '../services/tmdbApi';

const IMAGE_BASE_URL = 'https://image.tmdb.org/t/p/w780'; // Larger image for detail page

export default function MovieDetailPage() {
    const { id: movieId } = useParams(); // Get the movie ID from the URL
    const [movie, setMovie] = useState<Movie>();
    const [imageUrl, setImageUrl] = useState('https://dummyimage.com//780x1170?text=No+Image');
    const [isFavorite, setIsFavorite] = useState<boolean | null>(null);
    const sessionId = TMDB.getSessionId();
    const user = TMDB.getUser();
    

    const loadMovie = useCallback(async (movieId: string) => {
        const movieData = await TMDB.getMovieDetails(+movieId, sessionId);
        setMovie(movieData);
        setIsFavorite(movieData.account_states?.favorite);
        if (movieData.poster_path) {
            setImageUrl(`${IMAGE_BASE_URL}${movieData.poster_path}`);
        }
    }, [sessionId]);
    
    useEffect(() => {
        if (movieId == null) return;
        loadMovie(movieId);
    }, [movieId, loadMovie]);

    const handleFavoriteClick = async () => {
        if (!sessionId) {
            alert('Please log in to favorite movies.');
            return;
        }

        if (!movieId) return;
        
        const action = isFavorite ? false : true;
        await TMDB.addFavorite(+user.accountId, sessionId, +movieId, action);
        setIsFavorite(action);
    }

    if (!movie) return <div className="text-center"><p>Loading movie...</p></div>;

    return (
        <>
            <div className="movie-detail">
                <div className="flex flex-col md:flex-row gap-6 md:gap-8">
                    <div className="md:w-1/3 flex-shrink-0">
                        <img
                            src={imageUrl}
                            alt={movie.title}
                            className="rounded-lg shadow-lg w-full"
                        />
                    </div>
                    <div className="md:w-2/3">
                        <h1 className="text-3xl md:text-4xl font-bold mb-3">{movie.title}</h1>
                        {movie.tagline && <p className="text-lg italic text-gray-600 dark:text-gray-400 mb-4">{movie.tagline}</p>}
                        <div className="flex items-center mb-4 space-x-4">
                            <span className="text-yellow-400 flex items-center">
                                <svg className="w-5 h-5 mr-1" fill="currentColor" viewBox="0 0 20 20"><path d="M9.049 2.927c.3-.921 1.603-.921 1.902 0l1.07 3.292a1 1 0 00.95.69h3.462c.969 0 1.371 1.24.588 1.81l-2.8 2.034a1 1 0 00-.364 1.118l1.07 3.292c.3.921-.755 1.688-1.54 1.118l-2.8-2.034a1 1 0 00-1.175 0l-2.8 2.034c-.784.57-1.838-.197-1.539-1.118l1.07-3.292a1 1 0 00-.364-1.118L2.98 8.72c-.783-.57-.38-1.81.588-1.81h3.461a1 1 0 00.951-.69l1.07-3.292z"></path></svg>
                                {movie.vote_average ? movie.vote_average.toFixed(1) : 'N/A'}
                            </span>
                            <span>{movie.release_date ? `Release Date: ${movie.release_date}` : ''}</span>
                            <span>{movie.runtime ? `${movie.runtime} min` : ''}</span>
                        </div>
                        <div className="mb-4">
                            <h2 className="text-xl font-semibold mb-2">Genres</h2>
                            <div className="flex flex-wrap gap-2">
                                {movie.genres?.map((genre) => (
                                    <span key={genre.id} className="bg-gray-200 dark:bg-gray-700 text-sm px-3 py-1 rounded-full">
                                        {genre.name}
                                    </span>
                                ))}
                            </div>
                        </div>
                        <div className="mb-6">
                            <h2 className="text-xl font-semibold mb-2">Overview</h2>
                            <p className="text-gray-700 dark:text-gray-300 leading-relaxed">{movie.overview || 'No overview available.'}</p>
                        </div>
                        {sessionId ? (
                            <>
                                <button onClick={handleFavoriteClick}
                                    className={`px-4 py-2 rounded font-semibold transition duration-200 flex items-center ${isFavorite
                                        ? 'bg-red-500 hover:bg-red-600 text-white'
                                        : 'bg-green-500 hover:bg-green-600 text-white'
                                        }`}
                                >
                                    <svg className="w-5 h-5 mr-2" fill="currentColor" viewBox="0 0 20 20">
                                        {isFavorite ? (
                                            <path fillRule="evenodd" d="M3.172 5.172a4 4 0 015.656 0L10 6.343l1.172-1.171a4 4 0 115.656 5.656L10 17.657l-6.828-6.829a4 4 0 010-5.656z" clipRule="evenodd" />
                                        ) : (
                                            <path d="M3.172 5.172a4 4 0 015.656 0L10 6.343l1.172-1.171a4 4 0 115.656 5.656L10 17.657l-6.828-6.829a4 4 0 010-5.656z" /> // Outline for non-favorite
                                        )}
                                    </svg>
                                    {isFavorite ? 'Remove from Favorites' : 'Add to Favorites'}
                                </button>
                            </>
                        ) : (
                            <></>
                        )}
                        {!sessionId && (
                            <Link to="/login" className="text-blue-500 hover:underline">
                                Log in to add to favorites
                            </Link>
                        )}

                    </div>
                </div>

            </div>
        </>


    )
}

