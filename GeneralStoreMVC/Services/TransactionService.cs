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

            var product = await _context.Products.SingleOrDefaultAsync(p => p.Id == model.ProductId);

            if (product == null)
                return false;

            if (product.QuantityInStock < model.Quantity)
                return false;

            _context.Transactions.Add(new Transaction
            {
                ProductId = model.ProductId,
                CustomerId = model.CustomerId,
                Quantity = model.Quantity,
                DateOfTransaction = DateTime.Now
            });

            product.QuantityInStock -= model.Quantity;

            if (await _context.SaveChangesAsync() == 2)
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
            Product product;
            int changes = 1;

            if (transaction is null) return false;

            if (transaction.ProductId != model.ProductId)
            {
                // Since we are managing product inventory by taking away from it with transactions
                // We need to check if they changed what product it was. If so grab the old product
                // Add the product back in so it's not missing inventory
                // Before we do that though we should check we aren't taking more inventory then we have
                product = await _context.Products.FindAsync(model.ProductId);

                if (product is null) return false;
                if (product.QuantityInStock < model.Quantity) return false;

                product.QuantityInStock -= model.Quantity;
                changes += 1;
                var oldProduct = await _context.Products.FindAsync(transaction.ProductId);
                oldProduct.QuantityInStock += transaction.Quantity;
                changes += 1;
            }
            else
            {
                // If it's still the same product let's see if the quantity is changed
                // If so we'll add back the old value and take away the new value undoing anything we did
                if (transaction.Quantity != model.Quantity)
                {
                    product = await _context.Products.FindAsync(transaction.ProductId);
                    product.QuantityInStock += transaction.Quantity;
                    if (product.QuantityInStock < model.Quantity) return false;
                    product.QuantityInStock -= model.Quantity;
                    changes += 1;
                }
            }

            transaction.ProductId = model.ProductId;
            transaction.Quantity = model.Quantity;
            transaction.CustomerId = model.CustomerId;
            transaction.DateOfTransaction = model.DateOfTransaction;

            if (await _context.SaveChangesAsync() == changes)
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
