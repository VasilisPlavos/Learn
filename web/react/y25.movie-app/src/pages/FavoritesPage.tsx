import { Link } from "react-router-dom";
import MovieCard from "../components/MovieCard";
import { useEffect, useState } from "react";
import { Movie } from "../models/movie.model";
import { TMDB } from "../services/tmdbApi";

export default function FavoritesPage() {
  const [isLoading, setIsLoading] = useState(true);
  const [favoriteMovies, setFavoriteMovies] = useState<Movie[]>([]);
  const sessionId = TMDB.getSessionId();

  useEffect(() => {
    if (sessionId) TMDB.getFavoriteMovies(sessionId).then(x => setFavoriteMovies(x)).then(() => { setIsLoading(false) })
  }, [sessionId])

  if (!sessionId) {
    return (
      <div className="text-center p-10">
        <h1 className="text-2xl font-bold mb-4">My Favorite Movies</h1>
        <p>Please <Link to="/login" className="text-blue-500 hover:underline">log in</Link> to view your favorite movies.</p>
      </div>
    );
  }

  return (
    <div>
      <h1 className="text-3xl font-bold mb-6 text-center">My Favorite Movies</h1>
      {isLoading ? (
        <div className="text-center">
          {/* Optional: Replace with <LoadingSpinner /> */}
          <p>Loading favorites...</p>
        </div>
      ) : favoriteMovies.length > 0 ? (
        <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5 gap-6">
          {favoriteMovies.map((movie) => (
            <MovieCard key={movie.id} movie={movie} />
          ))}
        </div>
      ) : (
        <div className="text-center text-gray-500 dark:text-gray-400">
          <p>You haven't added any movies to your favorites yet.</p>
          <Link to="/" className="text-blue-500 hover:underline mt-2 inline-block">Browse movies</Link>
        </div>
      )}

      {/* TODO: Add pagination if favoritesData.total_pages > 1 */}
    </div>
  );


}