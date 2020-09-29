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
                new Payload() { Path = "file1__prioritized__2", Priority = 2 },
                new Payload() { Path = "file2__prioritized__6", Priority = 6 },
                new Payload() { Path = "file3__prioritized__3", Priority = 3 },
                new Payload() { Path = "file4__prioritized__2", Priority = 2 },
                new Payload() { Path = "file5__prioritized__5", Priority = 5 },
                new Payload() { Path = "file6__prioritized__7", Priority = 7 },
                new Payload() { Path = "file7__prioritized__1", Priority = 1 },
                new Payload() { Path = "file8__prioritized__5", Priority = 5 },
                new Payload() { Path = "file9__prioritized__6", Priority = 6 },
                new Payload() { Path = "file10__prioritized__2", Priority = 2 },
                new Payload() { Path = "file11__prioritized__3", Priority = 3 },
                new Payload() { Path = "file12__prioritized__2", Priority = 2 },
                new Payload() { Path = "file13__prioritized__1", Priority = 1 },
                new Payload() { Path = "file14__prioritized__10", Priority = 10 },
                new Payload() { Path = "file15__prioritized__5", Priority = 5 },
                new Payload() { Path = "file16__prioritized__2", Priority = 2 },
                new Payload() { Path = "file17__prioritized__5", Priority = 5 },
                new Payload() { Path = "file18__prioritized__4", Priority = 4 },
                new Payload() { Path = "file19__prioritized__10", Priority = 10 }
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