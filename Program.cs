using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HttpClientExample
{
    // Basic Patient class with some properties
    public class Patient
    {
        [Required, JsonPropertyName("patientName")]
        public string? PatientName { get; set; }
        [Required, JsonPropertyName("patientAge")]
        public int PatientAge { get; set; }
        [Required, JsonPropertyName("isActive")]
        public bool IsActive { get; set; }
        [Required, JsonPropertyName("dependents")]
        public List<Patient>? Dependents { get; set; }
    }

    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main()
        {
            string fileName = "example.json";
            string jsonString = File.ReadAllText(fileName);
            Patient alice = JsonSerializer.Deserialize<Patient>(jsonString)!;

            // For example purposes only, output the contents of the newly created object to the console
            Console.Write(JsonSerializer.Serialize(alice));

            // Create the HttpClient request
            var url = "https://patient.free.beeceptor.com";            
            var result = await client.PostAsync(url, JsonContent.Create(alice));

            // Output the HTTP status code
            Console.Write("API request status code: {0}", result.StatusCode);

            // Can also visit : https://beeceptor.com/console/patient to see the requests succeeding live.
        }
    }
}
