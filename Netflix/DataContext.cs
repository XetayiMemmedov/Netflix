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
        public List<Movie> Movies { get; set; }
        public List<Genre> Genres { get; set; }
        private List<User> _users;
        public List<Movie> Recentlyviewed {  get; set; }
        public List<Movie> Watchlist {  get; set; }


        public DataContext()
        {
            _users = new List<User>();
            Movies = new List<Movie>();
            Genres = new List<Genre>();
            _users.Add(new User() { Username = "admin", Password = "1234", Role = UserRole.Admin });
            _users.Add(new User() { Username = "spectator", Password = "1234", Role = UserRole.User });
            Genres.Add(new Genre("Bedii"));
            Genres.Add(new Genre("Romantik"));
            Genres.Add(new Genre("Psixolojik"));
            Movies.Add(new Movie("Şerikli cörek", Genres[0], 148));
            Movies.Add(new Movie("Axirinci asirim", Genres[0], 152));
            Movies.Add(new Movie("Titanik", Genres[1], 135));
            Movies.Add(new Movie("Otel otagi 1405", Genres[2], 110));
            Recentlyviewed = new List<Movie>();
            Watchlist = new List<Movie>();




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

        public void AddMovie(string moviename, Genre genre, int duration)
        {
            if (moviename != null && genre != null && duration > 0&& duration<1000)
            {
                var movie = Movies.FirstOrDefault(m => m.Title == moviename);
                if (movie == null)
                {
                    Movies.Add(new Movie(moviename, genre, duration));
                    Console.WriteLine($"{moviename} is successfully added to movielist.");
                }
                else
                {
                    Console.WriteLine($"Movie with the name {moviename} already exists.");
                }
            }
            else
            {
                Console.WriteLine("Movie, genre or duration cannot be empty or invalid input.");
            }

        }

        public void RemoveMovie(int id)
        {
            var movieToRemove = Movies.FirstOrDefault(m => m.Id == id);
            if (movieToRemove != null)
            {
                Movies.Remove(movieToRemove);
                Console.WriteLine($"Movie with Title {movieToRemove.Title} has been removed.");
                PrintHelper.PrintMovies(Movies);
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
                var genre = Genres.FirstOrDefault(m => m.GenreName == genrename);
                if (genre == null)
                {
                    Genres.Add(new Genre(genrename));
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
            var genreToRemove = Genres.FirstOrDefault(m => m.Id == id);
            if (genreToRemove != null)
            {
                Genres.Remove(genreToRemove);
                Console.WriteLine($"Genre with the name {genreToRemove.GenreName} has been removed.");
            }
            else
            {
                Console.WriteLine($"No genre found with Id {id}.");
            }

        }
        public Genre? GetGenre(int id)
        {
            foreach (var item in Genres)
            {
                if (item == null) continue;

                if (item.Id == id) return item;
            }
            return null;

        }

        public Movie? GetMovie(int id)
        {
            foreach (var item in Movies)
            {
                if (item == null) continue;

                if (item.Id == id) return item;
            }
            return null;

        }
        public Movie? GetMovieByName(string name)
        {
            foreach (var item in Movies)
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
            foreach (var item in Movies)
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
            foreach (var item in Movies)
            {
                if (item == null) continue;

                if (item.Id == id)
                {
                    var movie = Recentlyviewed.FirstOrDefault(m => m.Id == id);
                    if (movie == null)
                    {
                        Console.WriteLine($"You are watching {item.Title}... ");
                        item.ViewCount++;
                        Recentlyviewed.Add(item);
                    }
                }
            }

        }
        public void AddToWhatchlist(int id)
        {
            foreach (var item in Movies)
            {
                if (item == null) continue;

                if (item.Id == id)
                {
                    var movie = Watchlist.FirstOrDefault(m => m.Id == id);
                    if (movie == null)
                    {
                        Watchlist.Add(item);
                        Console.WriteLine($"{item.Title} is successfully added to watchlist.");

                    }
                }
            }
        }
       
    }
}
