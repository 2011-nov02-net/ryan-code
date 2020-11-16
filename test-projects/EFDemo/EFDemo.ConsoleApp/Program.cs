using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using EFDemo.ConsoleApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EFDemo.ConsoleApp
{
    class Program
    {
        static DbContextOptions<ChinookContext> s_dbContextOptions;

        static void Main(string[] args)
        {
            //build optioins to log in to db
            var optionsBuilder = new DbContextOptionsBuilder<ChinookContext>();
            optionsBuilder.UseSqlServer(GetConnectionString());
            optionsBuilder.LogTo(x => Debug.WriteLine(x), LogLevel.Information); //log to debug console
            s_dbContextOptions = optionsBuilder.Options;

            //display
            Display5Tracks();

            //edit
            Console.Write("\nEdit track name: ");
            string editInput = Console.ReadLine();

            EditSomeTracks(editInput);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nEdited Track");
            Console.ForegroundColor = ConsoleColor.Gray;
            Display5Tracks();

            //insert
            Console.Write("\nEnter new track name: ");
            string insertInput = Console.ReadLine();
            Console.Write("Enter new track genre (1-25): ");
            string insertGenreInput = Console.ReadLine();

            InsertATrack(insertInput, Convert.ToInt32(insertGenreInput));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nInserted Track");
            Console.ForegroundColor = ConsoleColor.Gray;
            Display5Tracks();

            //delete
            DeleteTrack();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nDeleted Inserted Track");

            //display
            Console.ForegroundColor = ConsoleColor.Gray;
            Display5Tracks();


            Console.WriteLine("\nDone");

        }

        static string GetConnectionString()
        {
            string path = "../../../chinnok-connect.json";
            string json = "";
            try
            {
                json = File.ReadAllText(path);
            }
            catch(IOException)
            {
                Console.WriteLine("Error reading connection string");
                throw;
            }

            return json;
        }

        static void Display5Tracks()
        {
            using var context = new ChinookContext(s_dbContextOptions);

            //get top 5 tracks
            IQueryable<Track> tracks = context.Tracks.Include(t => t.Genre).Include(t => t.Album).OrderBy(t => t.Name).Take(5);

            string header = string.Format("\n{0,-5}\t{1,-25}\t{2,-65}\t{3,-15}", "ID", "Name", "Album", "Ganre");
            Console.WriteLine(header);
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");

            //do stuff with table data
            foreach (var t in tracks)
            {
                string output = string.Format("{0,-5}\t{1,-25}\t{2,-65}\t{3,-15}", t.TrackId, t.Name, t.Album.Title, t.Genre.Name);
                Console.WriteLine(output);
                //Console.WriteLine($"{t.TrackId}\t{t.Name}\t{t.Album.Title}\t({t.Genre.Name})");
            }
        }

        static void EditSomeTracks(string newName)
        {
            using var context = new ChinookContext(s_dbContextOptions);

            //get top track
            IQueryable<Track> track = context.Tracks.Include(t => t.Genre).OrderBy(t => t.Name).Take(1);

            //Edit top track
            var t = track.First();
            t.Name = $"\"{newName}\"";

            //save edit
            context.SaveChanges();
        }

        static void InsertATrack(string name, int gid)
        {
            using (var context = new ChinookContext(s_dbContextOptions))
            {
                int lastId = 0;

                //get last id in table
                IQueryable<Track> track = context.Tracks.OrderByDescending(t => t.TrackId).Take(1);
                var t = track.First();
                lastId = t.TrackId;

                //create new track to add
                Track newTrack = new Track();
                newTrack.TrackId = lastId + 1;
                newTrack.Name = $"\"{name}\"";
                newTrack.GenreId = gid;
                newTrack.MediaTypeId = 1;

                //insert and save changes
                context.Tracks.Add(newTrack);
                context.SaveChanges();
            }
        }

        static void DeleteTrack()
        {
            using (var context = new ChinookContext(s_dbContextOptions))
            {
                //get created track - it should be last track id
                IQueryable<Track> track = context.Tracks.OrderByDescending(t => t.TrackId).Take(1);

                //delete track
                var t = track.First();
                context.Tracks.Remove(t);

                //save changes
                context.SaveChanges();
            }
        }
    }
}
