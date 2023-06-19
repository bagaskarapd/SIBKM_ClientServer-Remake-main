﻿using API.Models;
using API.ViewModels;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Client.Repositories
{
    public class AccountRepository
    {
        private readonly string request;
        private readonly HttpClient httpClient;

        public AccountRepository(string request = "Account/")
        {
            this.request = request;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7078/api/")
            };
        }

        public async Task<ResponseDataVM<string>> Login(LoginVM login)
        {
            ResponseDataVM<string> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            using (var response = httpClient.PostAsync(request + "login", content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseDataVM<string>>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseDataVM<List<Account>>> Get()
        {
            ResponseDataVM<List<Account>> entityVM = null;

            using (var response = await httpClient.GetAsync(request))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseDataVM<List<Account>>>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseDataVM<string>> Post(Account account)
        {
            ResponseDataVM<string> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(account), Encoding.UTF8, "application/json");
            using (var response = httpClient.PostAsync(request, content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseDataVM<string>>(apiResponse);
            }
            return entityVM;
        }
        public async Task<ResponseDataVM<Account>> Get(int id)
        {
            ResponseDataVM<Account> entity = null;

            using (var response = await httpClient.GetAsync(request + id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entity = JsonConvert.DeserializeObject<ResponseDataVM<Account>>(apiResponse);
            }
            return entity;
        }
        public async Task<ResponseDataVM<string>> Delete(int id)
        {
            ResponseDataVM<string> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");
            using (var response = httpClient.DeleteAsync(request + id).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseDataVM<string>>(apiResponse);
            }
            return entityVM;
        }
        public async Task<ResponseDataVM<string>> Put(int id, Universitie universitie)
        {
            ResponseDataVM<string> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(universitie), Encoding.UTF8, "application/json");
            using (var response = httpClient.PutAsync(request, content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseDataVM<string>>(apiResponse);
            }
            return entityVM;
        }
    }
}