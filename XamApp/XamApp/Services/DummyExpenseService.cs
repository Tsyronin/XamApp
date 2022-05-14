using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using XamApp.Models;
using Xamarin.Forms;

namespace XamApp.Services
{
    public class ExpenseService
    {
        HttpClient client;

        string baseUri = "https://192.168.5.106:44304/api/";

        string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJ1c2VyMUBnbWFpbC5jb20iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjljODRlNjkxLTMzM2QtNDQ4Yy04MTdhLWY4ZTZlNWQ5YzNlOSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IlVzZXIiLCJuYmYiOjE2NTIzODAzMDQsImV4cCI6MTY1MjM4NTcwNCwiaXNzIjoiV2ViQXBwbGljYXRpb253V2ViQVBJX0pXVF9kZW1vU2VydmVyIiwiYXVkIjoiV2ViQXBwbGljYXRpb253V2ViQVBJX0pXVF9kZW1vQ2xpZW50In0.N7jXotHM9fBEOlncXCkD1Y2dV3-R-TaNo9RG0T2jE4o";

        public ExpenseService()
        {
            client = new HttpClient(new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
                {
                    //bypass
                    return true;
                },
            }
            , false);
        }

        public async Task<IEnumerable<Expense>> GetNotCheckedExpensesAsync()
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(baseUri + "Expense/notchecked");

            string responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<Expense>>(responseString);
            return result;
        }

    }
}
