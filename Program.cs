using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Text.Json;

namespace HttpClientExample
{
    // Basic Patient class with some properties
    public class Patient
    {
        [Required]
        public string? PatientName { get; set; }
        [Required]
        public int PatientAge { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public List<Patient>? Dependents { get; set; }
    }

    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main()
        {
            // Instantiate the Patient class and assign some data
            Patient alice = new Patient()
            {
                PatientName = "Alice Newman",
                PatientAge = 48,
                IsActive = true,
                Dependents = new List<Patient>() {
                    new Patient{
                        PatientName = "Zeke Newman",
                        PatientAge = 19,
                        IsActive = true
                    },
                    new Patient
                    {
                        PatientName = "Claire Newman",
                        PatientAge = 14,
                        IsActive = false
                    } 
                }
            };

            // For example purposes only, output the contents of the object to the console
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
