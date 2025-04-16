import { useEffect, useState } from "react"
import { Movie } from '../models/movie.model';
import MovieCard from "../components/MovieCard";
import { TMDB } from '../services/tmdbApi';

export default function HomePage() {
    const [movies, setMovies] = useState<Movie[]>([]);

    useEffect(() => {
        TMDB.getPopularMovies().then(x => setMovies(x));

    }, [])

    return (
        <div>
            <h1 className="text-3xl font-bold mb-6 text-center">Popular Movies</h1>
            {movies?.length === 0 ? (
                <div className="text-center">
                    <p>Loading movies...</p>
                </div>
            ) : (
                <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5 gap-6">
                    {movies.map((movie) => (
                        <MovieCard key={movie.id} movie={movie} />
                    ))}
                </div>
            )}

            {/* TODO: Add pagination controls */}
        </div>
    )
}