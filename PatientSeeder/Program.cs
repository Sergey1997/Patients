using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Text.Json;

var client = new HttpClient { BaseAddress = new Uri(Environment.GetEnvironmentVariable("API_URL") ?? "http://localhost:5051/api/") };

Console.WriteLine("Waiting for API to be ready...");
await Task.Delay(10000);

var random = new Random();
var names = new[] { "Иван", "Алексей", "Мария", "Анна", "Дмитрий" };
var families = new[] { "Иванов", "Петров", "Сидоров", "Смирнов", "Фёдоров" };
var genders = new[] { "Male", "Female", "Other", "Unknown" };

for (int i = 0; i < 100; i++)
{
    var patient = new
    {
        Family = families[random.Next(families.Length)],
        Given = names[random.Next(names.Length)],
        BirthDate = DateTime.UtcNow.AddDays(-random.Next(365 * 20)).ToString("o"),
        Gender = genders[random.Next(genders.Length)],
        Active = random.Next(2) == 1
    };

    // Convert to JSON string for logging
    string patientJson = JsonSerializer.Serialize(patient);
    Console.WriteLine($"Sending patient data: {patientJson}");

    var response = await client.PostAsJsonAsync("patients", patient);
    Console.WriteLine($"Response: {await response.Content.ReadAsStringAsync()}");
}