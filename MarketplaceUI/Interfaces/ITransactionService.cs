using Marketplace.Models;
using MarketPlaceDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceUI.Interfaces
{
    public interface ITransactionService
    {
            Task<List<Transaction>> GetTransactionsAsync();
            Task<Transaction?> GetTransactionByIdAsync(int id);
            Task<Transaction> CreateTransactionAsync(Transaction transaction);
            Task<Transaction> UpdateTransactionAsync(Transaction transaction);
            Task<bool> DeleteTransactionAsync(int id);

    }
}
