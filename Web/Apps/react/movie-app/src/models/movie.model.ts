// export type Movie = {
//     adult: boolean;
//     backdrop_path: string;
//     
//     id: number;
//     original_language: string;
//     original_title: string;
//     overview: string;
//     popularity: number;
//     poster_path: string;
//     release_date: string;
//     title: string;
//     video: boolean;
//     vote_average: number;
//     vote_count: number;
// }


export interface Movie {
    id: number;
    genres: genre[];
    account_states: {
        favorite: boolean | null;
    };
    title: string;
    poster_path: string;
    overview: string;
    release_date: string;
    runtime: string;
    vote_average: number;
    tagline: string;
}

interface genre {
    id: number;
    name: string;
}