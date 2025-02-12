namespace Netflix
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var datacontext = new DataContext();
            
            UserRole currentRole = UserRole.User;
            bool isLoggedIn = false;
            while (true)
            {
                if (!isLoggedIn)
                {
                    Console.WriteLine("Enter username:");
                    string username = Console.ReadLine()?.Trim();
                    Console.WriteLine("Enter password:");
                    string password = Console.ReadLine()?.Trim();
                    if (datacontext.Login(username, password, out currentRole))
                    {
                        isLoggedIn = true;
                        if (currentRole == UserRole.Admin)
                        {
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.Clear();
                            Console.WriteLine("Successful login!");
                            Console.WriteLine("Welcome Admin");
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.Clear();
                            Console.WriteLine("Successful login!");
                            Console.WriteLine("Welcome Spectator");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Login failed. Invalid Username or password.");
                    }

                }
                else
                {
                    string choise;
                    if (currentRole == UserRole.Admin)
                    {
                        Console.WriteLine("Enter command:['add new movie', 'add new genre', 'remove movie', 'remove genre', 'most viewed'] \nEnter command:To exit or log out please type (exit/logout)");
                        choise = Console.ReadLine()?.ToLower();
                        if (choise == "add new movie")
                        {
                            string moviename = null;
                            Console.Write("Enter movie name:");
                            
                            while (true)
                            {
                                moviename= Console.ReadLine();
                                if (!string.IsNullOrEmpty(moviename))
                                    break;
                                else
                                    Console.WriteLine("Enter movie name");
                            }
                            int genreId;
                            string genrename = null;
                            while (true)
                            {
                                Console.WriteLine("Choose genre id below:");
                                PrintHelper.PrintGenres(datacontext._genres);
                                string input = Console.ReadLine();

                                if (int.TryParse(input, out genreId))
                                {
                                    var genre = datacontext.GetGenre(genreId);
                                    if (genre != null)
                                    {
                                        genrename = genre.GenreName;
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid genre ID. Please choose a valid genre.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Please enter a valid number for genre ID.");
                                }
                            }
                            int duration;
                            while (true)
                            {
                                Console.Write("Enter duration in minutes:");
                                string input = Console.ReadLine();
                                if (int.TryParse(input, out duration) && duration > 0)
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Please enter a valid positive number for duration.");
                                }
                            }
                            datacontext.AddMovie(moviename, genrename, duration);
                            PrintHelper.PrintMovies(datacontext._movies);
                        }
                        else if (choise == "remove movie")
                        {
                            Console.WriteLine("Enter movie id to remove:");
                            PrintHelper.PrintMovies(datacontext._movies);
                            string input = null;
                            int id;
                            while (true)
                            {
                                input = Console.ReadLine();
                                if (int.TryParse(input, out id))
                                {
                                    datacontext.RemoveMovie(id);
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Please enter a valid movie id.");
                                }
                            }
                        }
                        else if (choise == "add new genre")
                        {
                            Console.Write("Enter genre name:");
                            string genrename = Console.ReadLine();
                            datacontext.AddGenre(genrename);
                            PrintHelper.PrintGenres(datacontext._genres);
                        }
                        else if (choise == "remove genre")
                        {

                            Console.WriteLine("Enter genre id to remove:");
                            PrintHelper.PrintGenres(datacontext._genres);
                            string input = null;
                            int id;
                            while (true)
                            {
                                input = Console.ReadLine();
                                if (int.TryParse(input, out id))
                                {
                                    var genre = datacontext.GetGenre(id);
                                    if (genre != null)
                                    {
                                        datacontext.RemoveGenre(id);
                                        PrintHelper.PrintGenres(datacontext._genres);
                                        break;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Please enter a valid genre id.");
                                }
                            }
                        }
                        else if (choise == "most viewed")
                        {
                            var movie = datacontext.MostViewed();
                            PrintHelper.PrintMoviesById(datacontext._movies, movie.Id);
                        }
                        else if (choise == "exit")
                        {
                            Console.ResetColor();
                            Console.Clear();
                            Console.WriteLine("Exitting Netflix...We look forward to your next coming.");
                            break;
                        }
                        else if (choise == "logout")
                        {
                            Console.ResetColor();
                            Console.Clear();
                            Console.WriteLine("Logging out...We look forward to your next coming.");
                            isLoggedIn = false;
                            Console.WriteLine("To continue please login again.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice. Please type correct command.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Enter command:['add to watchlist', 'watch movie', 'filter movie by genre', 'search movie'] \nEnter command:To exit or log out please type (exit/logout)");
                        choise = Console.ReadLine()?.ToLower();
                        if (choise == "watch movie")
                        {
                            Console.WriteLine("Enter movie id to watch:");
                            PrintHelper.PrintMovies(datacontext._movies);
                            string input = null;
                            int id;
                            while (true)
                            {
                                input = Console.ReadLine();
                                if (int.TryParse(input, out id))
                                {
                                    var movie = datacontext.GetMovie(id);
                                    if (movie != null)
                                    {
                                        datacontext.WatchMovie(id);
                                        PrintHelper.PrintRecentlyViewed(datacontext._recentlytviewed);
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Please enter a valid movie id.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Please enter a valid movie id.");
                                }
                            }

                        }
                        else if (choise == "add to watchlist")
                        {
                            Console.WriteLine("Enter movie id to add to watchlist:");
                            PrintHelper.PrintMovies(datacontext._movies);
                            string input = null;
                            int id;
                            while (true)
                            {
                                input = Console.ReadLine();
                                if (int.TryParse(input, out id))
                                {
                                    var movie = datacontext.GetMovie(id);
                                    if (movie != null)
                                    {
                                        datacontext.AddToWhatchlist(id);
                                        PrintHelper.PrintWatchList(datacontext._watchlist);
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Please enter a valid movie id.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Please enter a valid movie id.");
                                }
                            }

                        }
                        else if (choise == "search movie")
                        {
                            Console.WriteLine("Enter movie name:");
                            string name = Console.ReadLine();
                            while (true)
                            {
                                if (name != null)
                                {
                                    var movie = datacontext.GetMovieByName(name);
                                    if (movie != null)
                                    {
                                        PrintHelper.PrintMoviesById(datacontext._movies, movie.Id);
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Enter existing movie name:");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Enter movie name:");
                                }
                            }
                        }
                        else if (choise == "filter movie by genre")
                        {
                            Console.WriteLine("Enter genre id to view movies of that genre:");
                            PrintHelper.PrintGenres(datacontext._genres);
                            int id;
                            string input = null;
                            while (true)
                            {
                                input = Console.ReadLine();
                                if (int.TryParse(input, out id))
                                {
                                    var genre = datacontext.GetGenre(id);
                                    if (genre != null)
                                    {
                                        PrintHelper.PrintMoviesByGenre(datacontext._movies, genre.GenreName);
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Enter valid genre id");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Enter valid genre id");
                                }
                            }
                        }
                        else if (choise == "exit")
                        {
                            Console.ResetColor();
                            Console.Clear();
                            Console.WriteLine("Exitting Netflix...We look forward to your next coming.");
                            break;
                        }
                        else if (choise == "logout")
                        {
                            Console.ResetColor();
                            Console.Clear();
                            Console.WriteLine("Logging out...We look forward to your next coming.");
                            isLoggedIn = false;
                            Console.WriteLine("To continue please login again.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice. Please type correct command.");
                        }
                    }
                }
            }
        }
    }
}
