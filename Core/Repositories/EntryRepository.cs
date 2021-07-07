using Phonebook.Models; 
using Phonebook.Core.IRepositories; 
using Phonebook.Data;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks; 
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Phonebook.Core.Repositories
{
    public class EntryRepository : GenericRepository<Entry>, IEntryRepository 
    {
        public EntryRepository(
            ApplicationDbContext context, 
            ILogger logger 
        ) : base(context, logger)
        {
            
        }

        public override async Task<IEnumerable<Entry>> All()
        {
            try
            {
                return await dbSet.ToListAsync(); 
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "{Repo} All method error", typeof(EntryRepository)); 
                return new List<Entry>(); 
            }
        }

        public override async Task<bool> Update(Entry entry)
        {


            try
            {
            var existingEntry = await dbSet.Where(x => x.Id == entry.Id)
                                            .FirstOrDefaultAsync(); 
            
            if(existingEntry == null)
                return await Add(entry); 
            
            existingEntry.Name = entry.Name; 
            existingEntry.PhoneNumber = entry.PhoneNumber; 

            return true; 
            }
            catch (System.Exception ex)
            {
                
                 _logger.LogError(ex, "{Repo} Update method error", typeof(EntryRepository)); 
                return false; 
            }
        }

        public override async Task<bool> Delete(Guid id)
        {
            try
            {
            var existingEntry = await dbSet.Where(x => x.Id == id)
                                            .FirstOrDefaultAsync(); 
            
            if(existingEntry == null)
                return false;
            
            dbSet.Remove(existingEntry); 
            return true; 
            }
            catch (System.Exception ex)
            {              
                 _logger.LogError(ex, "{Repo} Delete method error", typeof(EntryRepository)); 
                return false; 
            }
        }
    }
}