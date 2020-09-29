using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TestClient
{
    class Program
    {
        static void Main()
        {
            List<Payload> payloads = new List<Payload>
            {
                new Payload() { Path = "file1-prioritized-2", Priority = 2 },
                new Payload() { Path = "file2-prioritized-6", Priority = 6 },
                new Payload() { Path = "file3-prioritized-3", Priority = 3 },
                new Payload() { Path = "file4-prioritized-2", Priority = 2 },
                new Payload() { Path = "file5-prioritized-5", Priority = 5 },
                new Payload() { Path = "file6-prioritized-7", Priority = 7 },
                new Payload() { Path = "file7-prioritized-1", Priority = 1 },
                new Payload() { Path = "file8-prioritized-5", Priority = 5 },
                new Payload() { Path = "file9-prioritized-6", Priority = 6 },
                new Payload() { Path = "file10-prioritized-2", Priority = 2 },
                new Payload() { Path = "file11-prioritized-3", Priority = 3 },
                new Payload() { Path = "file12-prioritized-2", Priority = 2 },
                new Payload() { Path = "file13-prioritized-1", Priority = 1 },
                new Payload() { Path = "file14-prioritized-10", Priority = 10 },
                new Payload() { Path = "file15-prioritized-5", Priority = 5 },
                new Payload() { Path = "file16-prioritized-2", Priority = 2 },
                new Payload() { Path = "file17-prioritized-5", Priority = 5 },
                new Payload() { Path = "file18-prioritized-4", Priority = 4 },
                new Payload() { Path = "file19-prioritized-10", Priority = 10 }
            };

            for (int i = 0; i < payloads.Count; i++)
            {
                PrintDoc(payloads[i]).Wait();
            }
        }

        static async Task PrintDoc(Payload payload)
        {
            string apiAddress = "https://localhost:46973";
            string printEndpoint = "api/print";
            StringContent rmContent = new StringContent(
                JsonConvert.SerializeObject(payload),
                Encoding.UTF8, "application/json"
            );
            try
            {
                using var httpClient = new HttpClient();
                using var response = await httpClient.PostAsync(
                    $"{apiAddress}/{printEndpoint}",
                    rmContent
                );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}