using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using XamApp.Helpers;
using XamApp.Models;
using Xamarin.Forms;

namespace XamApp.Services
{
    public class ExpenseService
    {
        HttpClient client;

        string baseUri = "https://192.168.5.103:5001/";

        public string Token
        {
            get
            {
                return Settings.AccessToken;
            }
        }

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
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            try
            {
                var response = await client.GetAsync(baseUri + "api/Expense/notchecked");
                string responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Expense>>(responseString);
                return result;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            try
            {
                var response = await client.GetAsync(baseUri + "api/Category");
                string responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Category>>(responseString);
                return result;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task AddExpenseAsync(Expense expense)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            var jsonString = JsonConvert.SerializeObject(expense);

            var content = new StringContent(jsonString, UnicodeEncoding.UTF8, "application/json");
            try
            {
                var response = await client.PostAsync(baseUri + "api/Expense", content);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        internal async Task AddCategoryAsync(Category category)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            var jsonString = JsonConvert.SerializeObject(category);

            var content = new StringContent(jsonString, UnicodeEncoding.UTF8, "application/json");
            try
            {
                var response = await client.PostAsync(baseUri + "api/Category", content);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        internal async Task DeleteCategoryAsync(int categoryId)
        {
            try
            {
                var response = await client.DeleteAsync(baseUri + "api/Category/" + categoryId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Expense>> GetExpenseHistoryAsync()
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            try
            {
                var response = await client.GetAsync(baseUri + "api/Expense/history");
                string responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Expense>>(responseString);
                return result;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Statistic>> GetExpenseStatisticsAsync()
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            try
            {
                var response = await client.GetAsync(baseUri + "api/Expense/statistics");
                string responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Statistic>>(responseString);
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<string> LoginAsync(AuthModel authModel)
        {
            var jsonString = JsonConvert.SerializeObject(authModel);
            var requestContent = new StringContent(jsonString, UnicodeEncoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync(baseUri + "token", requestContent);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    JObject jwt = JsonConvert.DeserializeObject<JObject>(responseContent);

                    var accessToken = jwt.Value<string>("access_token");

                    return accessToken;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> RegisterAsync(AuthModel authModel)
        {
            var jsonString = JsonConvert.SerializeObject(authModel);
            var requestContent = new StringContent(jsonString, UnicodeEncoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync(baseUri + "register", requestContent);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteExpense(int expenseId)
        {
            try
            {
                var response = await client.DeleteAsync(baseUri + "api/Expense/" + expenseId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
