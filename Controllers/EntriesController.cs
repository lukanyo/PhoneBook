using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Phonebook.Core.IConfiguration;
using Phonebook.Models;

namespace Phonebook.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class EntriesController : ControllerBase
    {
        private readonly ILogger<EntriesController> _logger; 
        private readonly IUnitOfWork _unitOfWork ; 

        public EntriesController(
            ILogger<EntriesController> logger, 
            IUnitOfWork unitOfWork
        )
        {
            _logger = logger; 
            _unitOfWork = unitOfWork; 
        }

        [HttpPost]
        public async Task<IActionResult> CreateEntry(Entry entry)
        {
            Console.Write(entry); 
            if(ModelState.IsValid)
            {
                entry.Id = System.Guid.NewGuid(); 
                await _unitOfWork.Entries.Add(entry); 
                await _unitOfWork.CompleteAsync(); 

                return CreatedAtAction("GetItem", new {entry.Id}, entry); 

            }

            return new JsonResult("Something went wrong") { StatusCode = 500}; 
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var entries = await _unitOfWork.Entries.All();
            return Ok(entries); 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(Guid id)
        {
            var entry = await _unitOfWork.Entries.GetById(id);

            if(entry == null)
                return NotFound(); 
            
            return Ok(entry); 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(Guid id, Entry entry)
        {
            var existingEntry = await _unitOfWork.Entries.GetById(id);

            if(existingEntry == null){
                return BadRequest();
            }

            existingEntry.Name = entry.Name; 
            existingEntry.PhoneNumber = entry.PhoneNumber; 
                

            await _unitOfWork.Entries.Update(existingEntry); 
            await _unitOfWork.CompleteAsync(); 

            return NoContent(); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var entry = await _unitOfWork.Entries.GetById(id);

            if(entry == null){
                return BadRequest();
            }

            await _unitOfWork.Entries.Delete(id); 
            await _unitOfWork.CompleteAsync(); 

            return Ok(entry); 
        }
    }
}