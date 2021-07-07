using System; 
using System.ComponentModel.DataAnnotations;

namespace Phonebook.Models
{
    public class Entry
    {
        public Guid Id { get; set; }
        [Required()]
        public string Name { get; set; }
        [Required()]
        public string PhoneNumber { get; set; }
    }
}