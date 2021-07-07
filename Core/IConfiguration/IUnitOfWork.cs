using Phonebook.Core.IRepositories;  
using System.Threading.Tasks; 

namespace Phonebook.Core.IConfiguration{
    public interface IUnitOfWork
    {
        IEntryRepository Entries {get; }
        Task CompleteAsync(); 
    }
}