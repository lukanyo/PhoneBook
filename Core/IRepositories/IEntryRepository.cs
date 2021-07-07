using System; 
using System.Collections.Generic; 
using System.Threading.Tasks;
using Phonebook.Models;


namespace Phonebook.Core.IRepositories
{
    public interface IEntryRepository: IGenericRepository<Entry>
    {
    }
}