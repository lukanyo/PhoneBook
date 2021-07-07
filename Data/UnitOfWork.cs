using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Phonebook.Core.IConfiguration; 
using Phonebook.Core.IRepositories; 
using Phonebook.Core.Repositories; 

namespace Phonebook.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context; 
        private readonly ILogger _logger; 

        public IEntryRepository Entries { get; private set; }

        public UnitOfWork(
            ApplicationDbContext context, 
            ILoggerFactory loggerFactory
        )
        {
            _context = context; 
            _logger = loggerFactory.CreateLogger("logs"); 

            Entries = new EntryRepository(_context, _logger); 

        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}