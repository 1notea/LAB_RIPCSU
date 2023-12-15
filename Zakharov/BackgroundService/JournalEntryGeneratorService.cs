using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Zakharov.Models;

namespace Zakharov.BackgroundServices
{
    public class JournalEntryGeneratorService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Random _random = new Random();
        private readonly string[] _firstNames = { "Иван", "Анна", "Петр", "Елена", "Сергей", "Мария" };
        private readonly string[] _lastNames = { "Иванов", "Петров", "Сидоров", "Козлов", "Смирнов", "Кузнецов" };
        private readonly string[] _subjects = { "Математика", "Физика", "История", "ТДСУ", "РиИПКСУ" };

        public JournalEntryGeneratorService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    var journalService = scope.ServiceProvider.GetRequiredService<JournalService>();

                    var fullName = GenerateRandomFullName();
                    var subject = GenerateRandomSubject();
                    var comment = GenerateRandomComment();
                    var grade = GenerateRandomGrade();
                    var date = DateTime.UtcNow;

                    var entry = new Journal
                    {
                        StudentFullName = fullName,
                        SubjectName = subject,
                        Comment = comment,
                        Grade = grade,
                        Date = date
                    };

                    journalService.CreateJournal(entry);

                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                }
            }
        }

        private string GenerateRandomFullName()
        {
            var firstName = _firstNames[_random.Next(_firstNames.Length)];
            var lastName = _lastNames[_random.Next(_lastNames.Length)];
            return $"{firstName} {lastName}";
        }

        private string GenerateRandomSubject()
        {
            return _subjects[_random.Next(_subjects.Length)];
        }

        private string GenerateRandomComment()
        {
            return $"Комментарий к записи #{_random.Next(1, 100)}";
        }

        private int GenerateRandomGrade()
        {
            return _random.Next(26, 55);
        }
    }
}
