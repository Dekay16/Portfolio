using Portfolio.Context.Models;
using Portfolio.Data;
using System;

namespace Portfolio.SeedData
{
    public static class TrafficSeeder
    {
        public static void SeedTestData(ApplicationDbContext db)
        {
            // Remove this check to always reseed for testing
            // if (db.TrafficLogs.Any()) return;

            var random = new Random();
            var paths = new[] { "/", "/about", "/contact", "/products", "/blog" };

            var today = DateTime.UtcNow.Date;

            // Seed 30 days of data (past month)
            for (int day = 30; day >= 1; day--)
            {
                var date = today.AddDays(-day);

                // Random number of visits for the day
                int visits = random.Next(10, 50);

                for (int i = 0; i < visits; i++)
                {
                    db.TrafficLog.Add(new TrafficLog
                    {
                        PathAccessed = paths[random.Next(paths.Length)],
                        IpAddress = $"192.168.{random.Next(1, 255)}.{random.Next(1, 255)}",
                        UserAgent = "TestAgent/1.0",
                        TimeStamp = date
                            .AddHours(random.Next(0, 23))
                            .AddMinutes(random.Next(0, 59))
                            .AddSeconds(random.Next(0, 59))
                    });
                }
            }

            db.SaveChanges();
        }
    }
}
