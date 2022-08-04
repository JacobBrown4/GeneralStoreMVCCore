using GeneralStoreMVC.Models.Transaction;

namespace GeneralStoreMVC.Services
{
    public interface ITransactionService
    {
        Task<bool> CreateTransaction(TransactionCreate model);
        Task<IEnumerable<TransactionListItem>> GetAllTransactions();
        Task<TransactionDetail> GetTransactionById(int transactionId);
        Task<bool> UpdateTransaction(TransactionEdit model);
        Task<bool> DeleteTransaction(int transactionId);
    }
}
