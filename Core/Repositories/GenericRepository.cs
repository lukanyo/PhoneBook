using Phonebook.Core.IRepositories; 
using Phonebook.Data; 
using Microsoft.EntityFrameworkCore; 
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace Phonebook.Core.Repositories
{
    public class GenericRepository<T>: IGenericRepository<T> where T: class
    {
        protected ApplicationDbContext _context; 
        protected DbSet<T> dbSet; 
        protected readonly ILogger _logger; 

        public GenericRepository(
            ApplicationDbContext context, 
            ILogger logger
        )
        {
            _context = context; 
            _logger = logger; 
            this.dbSet = context.Set<T>(); 
        }

        public virtual async Task<IEnumerable<T>> All()
        {
            return await dbSet.ToListAsync(); 
        }

        public virtual async Task<T> GetById(Guid id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<bool> Add(T entry)
        {
            await dbSet.AddAsync(entry); 
            return true; 
        }

        public virtual Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> Update(T entry)
        {
            throw new NotImplementedException();
        }
    }
}