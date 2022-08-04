using GeneralStoreMVC.Data;
using GeneralStoreMVC.Models.Transaction;
using Microsoft.EntityFrameworkCore;

namespace GeneralStoreMVC.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly GeneralStoreDbContext _context;
        public TransactionService(GeneralStoreDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateTransaction(TransactionCreate model)
        {
            if (model == null) return false;

            _context.Transactions.Add(new Transaction
            {
                ProductId = model.ProductId,
                CustomerId = model.CustomerId,
                Quantity = model.Quantity,
                DateOfTransaction = DateTime.Now
            });

            if (await _context.SaveChangesAsync() == 1)
                return true;
            return false;
        }


        public async Task<TransactionDetail> GetTransactionById(int transactionId)
        {
            var transaction = await _context.Transactions
                .Include(r => r.Product)
                .Include(r => r.Customer)
                .SingleOrDefaultAsync(t => t.Id == transactionId);
            if (transaction is null)
            {
                return null;
            }

            return new TransactionDetail
            {
                Id = transaction.Id,
                ProductId = transaction.ProductId,
                Product = transaction.Product.Name,
                CustomerId = transaction.CustomerId,
                Customer = transaction.Customer.Name,
                Quantity = transaction.Quantity,
                DateOfTransaction = transaction.DateOfTransaction
            };
        }

        public async Task<IEnumerable<TransactionListItem>> GetAllTransactions()
        {
            var transactions = await _context.Transactions
                .Include(r => r.Product)
                .Include(r => r.Customer)
                .Select(transaction => new TransactionListItem
            {
                Id = transaction.Id,
                Product = transaction.Product.Name,
                Customer = transaction.Customer.Name,
                Quantity = transaction.Quantity,
                DateOfTransaction = transaction.DateOfTransaction
            }).ToListAsync();
            return transactions;
        }

        public async Task<bool> UpdateTransaction(TransactionEdit model)
        {
            var transaction = await _context.Transactions.FindAsync(model.Id);

            if (transaction is null) return false;

            transaction.ProductId = model.ProductId;
            transaction.Quantity = model.Quantity;
            transaction.CustomerId = model.CustomerId;
            transaction.DateOfTransaction = model.DateOfTransaction;

            if (await _context.SaveChangesAsync() == 1)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteTransaction(int transactionId)
        {
            var transaction = await _context.Transactions.FindAsync(transactionId);

            if (transaction is null) return false;

            _context.Transactions.Remove(transaction);
            if (await _context.SaveChangesAsync() == 1)
            {
                return true;
            }
            return false;
        }
    }
}
