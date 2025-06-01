import { BrowserRouter as Router, Route, Routes } from 'react-router-dom'
import './App.css'
import Header from './components/Header'
import HomePage from './pages/HomePage'
import MovieDetailPage from './pages/MovieDetailPage'
import LoginPage from './pages/LoginPage'
import AuthCallbackPage from './pages/AuthCallbackPage'
import FavoritesPage from './pages/FavoritesPage'
import TestsPage from './pages/TestsPage'

function App() {

  return (
    <Router>
      <Header /> {/* Add the Header component here */}
      <div className="container mx-auto p-4"> {/* Basic container styling */}
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/movie/:id" element={<MovieDetailPage />} />
          <Route path="/login" element={<LoginPage />} />
          <Route path="/favorites" element={<FavoritesPage />} />
          <Route path="/auth/approved" element={<AuthCallbackPage />} />
          <Route path='/tests' element={<TestsPage />} />
          <Route path="*" element={<div>404 Not Found</div>} /> {/* Catch-all route */}
        </Routes>
      </div>
      {/* Optional React Query Devtools */}
      {/* <ReactQueryDevtools initialIsOpen={false} /> */}
    </Router>
  )
}

export default App
