using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Netflix
{
    internal class PrintHelper
    {
        internal static void PrintGenres(List<Genre> genres)
        {
            if (genres == null || genres.Count == 0)
            {
                Console.WriteLine("No genres to display.");
                return;
            }

            Console.WriteLine(new string('-', 40));
            Console.WriteLine($"{"Id",-10}{"Name",-30}");
            Console.WriteLine(new string('-', 40));

            foreach (Genre genre in genres)
            {
                if (genre == null) continue;
                Console.WriteLine($"{genre.Id,-10}{genre.GenreName,-30}");
            }

            Console.WriteLine(new string('-', 40));
        }
        internal static void PrintMovies(List<Movie> movies)
        {
            if (movies == null || movies.Count == 0)
            {
                Console.WriteLine("No movies to display.");
                return;
            }

            int maxWidth = 40;
            for (int i = 0; i < movies.Count; i += 2) 
            {
                string movie1Id = movies[i].Id.ToString();
                string movie1Name = movies[i].Title;
                string movie1Genre = movies[i].GenrE.GenreName;
                string movie1Duration = movies[i].Duration + " min";
                string movie1Views = movies[i].ViewCount.ToString();

                string movie2Id = (i + 1 < movies.Count) ? movies[i + 1].Id.ToString() : "";
                string movie2Name = (i + 1 < movies.Count) ? movies[i + 1].Title : "";
                string movie2Genre = (i + 1 < movies.Count) ? movies[i + 1].GenrE.GenreName : "";
                string movie2Duration = (i + 1 < movies.Count) ? movies[i + 1].Duration + " min" : "";
                string movie2Views = (i + 1 < movies.Count) ? movies[i + 1].ViewCount.ToString() : "";

                int dd;
                dd = movies.Count - i;
                if (dd == 1)
                {
                    PrintMovieFrame1(movie1Id, movie1Name, movie1Genre, movie1Duration, movie1Views, maxWidth);
                    Console.Write(" "); 
                }
                else
                {
                    PrintMovieFrame2(movie1Id, movie1Name, movie1Genre, movie1Duration, movie1Views, maxWidth, movie2Id, movie2Name, movie2Genre, movie2Duration, movie2Views);
                }

            }
        }

        private static void PrintMovieFrame1(string id, string name, string genre, string duration, string views, int width)
        {
            int padding = width;
            Console.WriteLine(" "+new string('-', width));
            PrintCenteredText("ID: " + id, padding);
            PrintCenteredText("Name: " + name, padding);
            PrintCenteredText("Genre: " + genre, padding);
            PrintCenteredText("Duration: " + duration, padding);
            PrintCenteredText("Views: " + views, padding);
            Console.WriteLine(" "+new string('-', width));
        }

        private static void PrintCenteredText(string text, int padding)
        {
            int leftPadding = (padding - text.Length) / 2;
            int rightPadding = padding - text.Length - leftPadding;

            Console.WriteLine("|" + new string(' ', leftPadding) + text + new string(' ', rightPadding) + "|");
        }
        private static void PrintMovieFrame2(string id, string name, string genre, string duration, string views, int width, string id1, string name1, string genre1, string duration1, string views1)
        {
            int padding = width;
            Console.WriteLine(" " + new string('-', width)+ "   " + new string('-', width));
            PrintCenteredText2("ID: " + id, "ID: " + id1, padding);
            PrintCenteredText2("Name: " + name, "Name: " + name1, padding);
            PrintCenteredText2("Genre: " + genre, "Genre: " + genre1, padding);
            PrintCenteredText2("Duration: " + duration, "Duration: " + duration1, padding);
            PrintCenteredText2("Views: " + views, "Views: " + views1, padding);
            Console.WriteLine(" " + new string('-', width) + "   " + new string('-', width));
        }

        private static void PrintCenteredText2(string text1, string text2, int padding)
        {
            int leftPadding = (padding - text1.Length) / 2;
            int rightPadding = padding - text1.Length - leftPadding;
            int leftPadding2 = (padding - text2.Length) / 2;
            int rightPadding2 = padding - text2.Length - leftPadding2;

            Console.WriteLine("|" + new string(' ', leftPadding) + text1 + new string(' ', rightPadding) + "|"+ " " + "|" + new string(' ', leftPadding2) + text2 + new string(' ', rightPadding2) + "|");
        }

        
        internal static void PrintMoviesById(List<Movie> movies, int id)
        {
            if (movies == null || movies.Count == 0)
            {
                Console.WriteLine("No movies to display.");
                return;
            }
            bool flag = false;
            foreach (Movie movie in movies)
            {
                if (movie == null) continue;
                else if (movie.Id == id)
                {
                    int maxWidth = 40;
                    string movie1Id = movie.Id.ToString();
                    string movie1Name = movie.Title;
                    string movie1Genre = movie.GenrE.GenreName;
                    string movie1Duration = movie.Duration + " min";
                    string movie1Views = movie.ViewCount.ToString();
                    PrintMovieFrame1(movie1Id, movie1Name, movie1Genre, movie1Duration, movie1Views, maxWidth);
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                Console.WriteLine("No movie found");
            }

        }
        internal static void PrintMoviesByGenre(List<Movie> movies, string genre)
        {
            int count = 0;
            foreach (Movie movie in movies)
            {
                if (movie == null) continue;
                else if (movie.GenrE.GenreName == genre)
                {
                    count++;
                }
            }
            if (count > 0)
            {
                for (int i = 0; i < count; i += 2)
                {
                    int maxWidth = 40;
                    string movie1Id = movies[i].Id.ToString();
                    string movie1Name = movies[i].Title;
                    string movie1Genre = movies[i].GenrE.GenreName;
                    string movie1Duration = movies[i].Duration + " min";
                    string movie1Views = movies[i].ViewCount.ToString();

                    string movie2Id = (i + 1 < movies.Count) ? movies[i + 1].Id.ToString() : "";
                    string movie2Name = (i + 1 < movies.Count) ? movies[i + 1].Title : "";
                    string movie2Genre = (i + 1 < movies.Count) ? movies[i + 1].GenrE.GenreName : "";
                    string movie2Duration = (i + 1 < movies.Count) ? movies[i + 1].Duration + " min" : "";
                    string movie2Views = (i + 1 < movies.Count) ? movies[i + 1].ViewCount.ToString() : "";

                    int dd;
                    dd = count - i;
                    if (dd == 1)
                    {
                        PrintMovieFrame1(movie1Id, movie1Name, movie1Genre, movie1Duration, movie1Views, maxWidth);
                        Console.Write(" ");
                    }
                    else
                    {
                        PrintMovieFrame2(movie1Id, movie1Name, movie1Genre, movie1Duration, movie1Views, maxWidth, movie2Id, movie2Name, movie2Genre, movie2Duration, movie2Views);
                    }

                }


            }
            else
            {
                Console.WriteLine("No movie found.");
            }
            
        }
    }
}
