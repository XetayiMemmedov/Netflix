using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Netflix
{
    internal class DataContext
    {
        public List<Movie> _movies;
        public List<Genre> _genres;
        private List<User> _users;
        public List<History> _recentlytviewed;
        public List<WatchList> _watchlist;


        public DataContext()
        {
            _users = new List<User>();
            _movies = new List<Movie>();
            _genres = new List<Genre>();
            _users.Add(new User() { Username = "admin", Password = "1234", Role = UserRole.Admin });
            _users.Add(new User() { Username = "spectator", Password = "1234", Role = UserRole.User });
            _genres.Add(new Genre("Bedii"));
            _genres.Add(new Genre("Romantik"));
            _genres.Add(new Genre("Psixolojik"));
            _movies.Add(new Movie("Şerikli cörek", _genres[0].GenreName, 148));
            _movies.Add(new Movie("Axirinci asirim", _genres[0].GenreName, 152));
            _movies.Add(new Movie("Titanik", _genres[1].GenreName, 135));
            _movies.Add(new Movie("Otel otagi 1405", _genres[2].GenreName, 110));
            _recentlytviewed = new List<History>();
            _watchlist = new List<WatchList>();




        }

        public bool Login(string username, string password, out UserRole role)
        {
            var user = _users.FirstOrDefault(u => string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase) && u.Password == password);
            if (user != null)
            {
                role = user.Role;
                return true;
            }
            role = UserRole.User;
            return false;
        }

        public void AddMovie(string moviename, string genrename, int duration)
        {
            if (moviename != null && genrename != null && duration > 0)
            {
                var movie = _movies.FirstOrDefault(m => m.Title == moviename);
                if (movie == null)
                {
                    _movies.Add(new Movie(moviename, genrename, duration));
                    Console.WriteLine($"{moviename} is successfully added to movielist.");
                }
                else
                {
                    Console.WriteLine($"Movie with the name {moviename} already exists.");
                }
            }
            else
            {
                Console.WriteLine("Movie, genre or duration cannot be empty.");
            }

        }

        public void RemoveMovie(int id)
        {
            var movieToRemove = _movies.FirstOrDefault(m => m.Id == id);
            if (movieToRemove != null)
            {
                _movies.Remove(movieToRemove);
                Console.WriteLine($"Movie with Title {movieToRemove.Title} has been removed.");
                PrintHelper.PrintMovies(_movies);
            }
            else
            {
                Console.WriteLine($"No movie found with Id {id}.");
            }

        }

        public void AddGenre(string genrename)
        {
            if (genrename != null)
            {
                var genre = _genres.FirstOrDefault(m => m.GenreName == genrename);
                if (genre == null)
                {
                    _genres.Add(new Genre(genrename));
                    Console.WriteLine($"{genrename} is successfully added to genre list.");
                }
                else
                {
                    Console.WriteLine($"Genre with the name {genrename} already exists.");
                }
            }
            else
            {
                Console.WriteLine("Genre name cannot be empty.");
            }

        }
        public void RemoveGenre(int id)
        {
            var genreToRemove = _genres.FirstOrDefault(m => m.Id == id);
            if (genreToRemove != null)
            {
                _genres.Remove(genreToRemove);
                Console.WriteLine($"Genre with the name {genreToRemove.GenreName} has been removed.");
            }
            else
            {
                Console.WriteLine($"No genre found with Id {id}.");
            }

        }
        public Genre? GetGenre(int id)
        {
            foreach (var item in _genres)
            {
                if (item == null) continue;

                if (item.Id == id) return item;
            }
            return null;

        }

        public Movie? GetMovie(int id)
        {
            foreach (var item in _movies)
            {
                if (item == null) continue;

                if (item.Id == id) return item;
            }
            return null;

        }
        public Movie? GetMovieByName(string name)
        {
            foreach (var item in _movies)
            {
                if (item == null) continue;
                else if (item.Title == name)
                {
                    return item;
                }
            }
            return null;
        }
        public Movie? MostViewed()
        {
            int max = 0;
            Movie movie;
            foreach (var item in _movies)
            {
                if (item == null) continue;

                if (item.ViewCount > max)
                {
                    max = item.ViewCount;
                    movie = item;
                }
            }
            return null;

        }

        public void WatchMovie(int id)
        {
            foreach (var item in _movies)
            {
                if (item == null) continue;

                if (item.Id == id)
                {
                    var movie = _recentlytviewed.FirstOrDefault(m => m.Id == id);
                    if (movie == null)
                    {
                        Console.WriteLine($"You are watching {item.Title}... ");
                        item.ViewCount++;
                        _recentlytviewed.Add(new History(item.Title, item.GenreName, item.Duration));
                    }
                }
            }

        }
        public void AddToWhatchlist(int id)
        {
            foreach (var item in _movies)
            {
                if (item == null) continue;

                if (item.Id == id)
                {
                    var movie = _watchlist.FirstOrDefault(m => m.Id == id);
                    if (movie == null)
                    {
                        _watchlist.Add(new WatchList(item.Title, item.GenreName, item.Duration));
                        Console.WriteLine($"{item.Title} is successfully added to watchlist.");

                    }
                }
            }
        }
       
    }
}
