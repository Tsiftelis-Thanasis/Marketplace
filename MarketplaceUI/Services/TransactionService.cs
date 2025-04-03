using Marketplace.Models;
using MarketplaceUI.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MarketplaceUI.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly HttpClient _httpClient;

        public TransactionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Transaction>> GetTransactionsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Transaction>>("api/transaction");
        }

        public async Task<Transaction?> GetTransactionByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Transaction>($"api/transaction/{id}");
        }

        public async Task<Transaction> CreateTransactionAsync(Transaction transaction)
        {
            var response = await _httpClient.PostAsJsonAsync("api/transaction", transaction);
            return await response.Content.ReadFromJsonAsync<Transaction>();
        }

        public async Task<Transaction> UpdateTransactionAsync(Transaction transaction)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/transaction/{transaction.TransactionId}", transaction);
            return await response.Content.ReadFromJsonAsync<Transaction>();
        }

        public async Task<bool> DeleteTransactionAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/transaction/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}