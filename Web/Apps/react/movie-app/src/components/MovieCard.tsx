import { Link } from "react-router-dom";
import { Movie } from "../models/movie.model";
const IMAGE_BASE_URL = 'https://image.tmdb.org/t/p/w500'; // Base URL for TMDB images

interface MovieCardProps {
    movie: Movie;
}

export default function MovieCard({ movie }: MovieCardProps) {
    const imageUrl = movie.poster_path
        ? `${IMAGE_BASE_URL}${movie.poster_path}`
        : 'https://dummyimage.com//500x750?text=No+Image'; // Placeholder if no poster
    return (
        <div className="bg-white dark:bg-gray-800 rounded-lg shadow-md overflow-hidden transform transition duration-300 hover:scale-105">
            <Link to={`/movie/${movie.id}`}>
                <img
                    src={imageUrl}
                    alt={movie.title}
                    className="w-full h-auto object-cover" // Adjust height as needed
                    loading="lazy" // Lazy load images
                />
                <div className="p-4">
                    <h3 className="font-bold text-lg mb-1 truncate" title={movie.title}>
                        {movie.title}
                    </h3>
                    <p className="text-sm text-gray-600 dark:text-gray-400 mb-2">
                        Release Date: {movie.release_date || 'N/A'}
                    </p>
                    <div className="flex items-center">
                        <svg className="w-4 h-4 text-yellow-400 mr-1" fill="currentColor" viewBox="0 0 20 20">
                            <path d="M9.049 2.927c.3-.921 1.603-.921 1.902 0l1.07 3.292a1 1 0 00.95.69h3.462c.969 0 1.371 1.24.588 1.81l-2.8 2.034a1 1 0 00-.364 1.118l1.07 3.292c.3.921-.755 1.688-1.54 1.118l-2.8-2.034a1 1 0 00-1.175 0l-2.8 2.034c-.784.57-1.838-.197-1.539-1.118l1.07-3.292a1 1 0 00-.364-1.118L2.98 8.72c-.783-.57-.38-1.81.588-1.81h3.461a1 1 0 00.951-.69l1.07-3.292z"></path>
                        </svg>
                        <span className="text-sm font-semibold">
                            {movie.vote_average ? movie.vote_average.toFixed(1) : 'N/A'}
                        </span>
                    </div>
                </div>
            </Link>
        </div>
    );
}