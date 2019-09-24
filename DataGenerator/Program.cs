using DataGenerator.JsonProxy;
using DataGenerator.Properties;
using Microsoft.EntityFrameworkCore;
using Shared.Data.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;

namespace DataGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (StreamReader sr = new StreamReader(@"JsonData\schedule_full.json"))
                {
                    String json = sr.ReadToEnd();
                    var episodes = Episode.FromJson(json);
                    var shows = episodes.Select(episode => episode.Embedded.Show).ToList();

                    //downloadAllCast(shows);

                    insertToDB(shows);

                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read");
                Console.WriteLine(e.Message);
            }
        }

        static void insertToDB(List<Show> shows)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RtlDbContext>();
            optionsBuilder.UseSqlServer(Resources.DefaultConnection);

            var dbContext = new RtlDbContext(optionsBuilder.Options);

            dbContext.Database.EnsureCreated();

            var dbShows = new List<ShowEntity>();

            foreach (var show in shows)
            {
                var showEntity = new ShowEntity();
                showEntity.Name = show.Name;
                showEntity.CorrelationId = show.Id;

                showEntity.Actors = new List<ActorEntity>();

                List<Actor> actors = readPersonFile(show.Id);

                foreach (var actor in actors)
                {
                    ActorEntity actorEntity = new ActorEntity();
                    actorEntity.Name = actor.Person.Name;
                    actorEntity.CorrelationId = actor.Person.Id;
                    if (actor.Person.Birthday.HasValue)
                    {
                        actorEntity.Birthday = actor.Person.Birthday.Value.Date;
                    }

                    showEntity.Actors.Add(actorEntity);
                }

                dbShows.Add(showEntity);
            }

            dbContext.Shows.AddRange(dbShows);
            dbContext.SaveChanges();
        }

        static List<Actor> readPersonFile(long showId)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                String.Format(@"JsonData\shows\show_{0}.json", showId));
            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    String json = sr.ReadToEnd();
                    var persons = Actor.FromJson(json);
                    return persons;
                }
            }
            else
            {
                return new List<Actor>();
            }
        }

        static void downloadAllCast(List<Show> shows)
        {
            foreach (var show in shows)
            {
                downloadCast(show.Id);
                Thread.Sleep(1000);
            }
        }

        static void downloadCast(long showId)
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(String.Format("http://api.tvmaze.com/shows/{0}/cast", showId),
                    String.Format(@"shows\show_{0}.json", showId));
            }
        }
    }
}
