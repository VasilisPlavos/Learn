// tmdbApi.ts
import axios from "axios";
import { Movie } from "../models/movie.model";
import { UserAccount } from "../models/userAccount.model";

const TMDB_API_KEY = ''; // Your TMDB API Key
const BASE_URL = 'https://api.themoviedb.org/3';

const tmdb = axios.create({
  baseURL: BASE_URL,
  params: {
    api_key: TMDB_API_KEY,
  },
});

// Utility to handle GET requests with additional params
const get = async (url: string, params = {}) => {
  const response = await tmdb.get(url, { params });
  return response.data;
};

export const TMDB = {
  createRequestToken: () => get("/authentication/token/new"),
  createSession: (requestToken: string) =>
    tmdb.post("/authentication/session/new", { request_token: requestToken }),

  getAccountDetails: (sessionId: string): Promise<UserAccount> =>
    get("/account", { session_id: sessionId }),

  getUser: () => {
    const user = localStorage.getItem('user');
    return user ? JSON.parse(user) : null;
  },

  getSessionId: (): string => {
    const userString = localStorage.getItem('user');
    const user = userString ? JSON.parse(userString) : null;
    return user?.sessionId ?? '';
  },

  logout: (): void => {
    localStorage.removeItem('user');
  },

  getFavoriteMovies: async (sessionId: string, page = 1) : Promise<Movie[]> =>{
    const account = await get("/account", { session_id: sessionId })
    const result = await get(`/account/${account.id}/favorite/movies`, {
      session_id: sessionId,
      page,
    })

    return result.results;
  },

  addFavorite: (
    accountId: number,
    sessionId: string,
    mediaId: number,
    favorite: boolean
  ) =>
    tmdb.post(
      `/account/${accountId}/favorite`,
      {
        media_type: "movie",
        media_id: mediaId,
        favorite,
      },
      {
        params: {
          session_id: sessionId,
        },
      }
    ),


  // 🟦 Configuration
  getConfiguration: () => get("/configuration"),

  // 🟨 Search
  searchMovies: (query: string, page = 1) =>
    get("/search/movie", { query, page }),
  searchTV: (query: string, page = 1) => get("/search/tv", { query, page }),
  searchPeople: (query: string, page = 1) =>
    get("/search/person", { query, page }),

  // 🎬 Movies
  getMovieDetails: (movieId: number, sessionId: string ) => get(`/movie/${movieId}`, { append_to_response: 'account_states', session_id: sessionId }),
  getMovieCredits: (movieId: number) => get(`/movie/${movieId}/credits`),
  getPopularMovies: async (page = 1) : Promise<Movie[]> => {
    const result = await get("/movie/popular", { page });
    return result.results;
  },
  getTopRatedMovies: (page = 1) => get("/movie/top_rated", { page }),
  getUpcomingMovies: (page = 1) => get("/movie/upcoming", { page }),
  getNowPlayingMovies: (page = 1) => get("/movie/now_playing", { page }),
  getMovieVideos: (movieId: number) => get(`/movie/${movieId}/videos`),
  getMovieImages: (movieId: number) => get(`/movie/${movieId}/images`),

  // 📺 TV Shows
  getTVDetails: (tvId: number) => get(`/tv/${tvId}`),
  getTVCredits: (tvId: number) => get(`/tv/${tvId}/credits`),
  getPopularTV: (page = 1) => get("/tv/popular", { page }),
  getTopRatedTV: (page = 1) => get("/tv/top_rated", { page }),
  getAiringTodayTV: (page = 1) => get("/tv/airing_today", { page }),
  getOnTheAirTV: (page = 1) => get("/tv/on_the_air", { page }),
  getTVSeasonDetails: (tvId: number, seasonNumber: number) =>
    get(`/tv/${tvId}/season/${seasonNumber}`),
  getTVSeasonCredits: (tvId: number, seasonNumber: number) =>
    get(`/tv/${tvId}/season/${seasonNumber}/credits`),

  // 👤 People
  getPersonDetails: (personId: number) => get(`/person/${personId}`),
  getPersonMovieCredits: (personId: number) =>
    get(`/person/${personId}/movie_credits`),
  getPersonTVCredits: (personId: number) =>
    get(`/person/${personId}/tv_credits`),

  // 🏷️ Genres
  getMovieGenres: () => get("/genre/movie/list"),
  getTVGenres: () => get("/genre/tv/list"),

  // 🧭 Discover
  discoverMovies: (filters = {}) => get("/discover/movie", filters),
  discoverTVShows: (filters = {}) => get("/discover/tv", filters),

  // 🔍 Trending
  getTrending: (mediaType: "all" | "movie" | "tv" | "person", timeWindow: "day" | "week") =>
    get(`/trending/${mediaType}/${timeWindow}`),
};
