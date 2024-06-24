using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Laboratory_Management_API.DataConnection;
using Laboratory_Management_API.Models; // Replace with your actual namespace
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Laboratory_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly DbContextClass _context;

        public TransactionController(DbContextClass context)
        {
            _context = context;
        }

        // GET: api/Transaction
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionDto>>> GetTransactions()
        {
            var transactions = await _context.Transactions
                .Select(transaction => new TransactionDto
                {
                    Transaction_Id = transaction.Transaction_Id,
                    Item_Id = transaction.Item_Id,
                    User_Id = transaction.User_Id, // Include User_Id
                    Transaction_Type = transaction.Transaction_Type,
                    Quantity = transaction.Quantity,
                    Transaction_Date = transaction.Transaction_Date,
                    Notes = transaction.Notes
                })
                .ToListAsync();

            return transactions;
        }

        // GET: api/Transaction/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionDto>> GetTransaction(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }

            var transactionDto = new TransactionDto
            {
                Transaction_Id = transaction.Transaction_Id,
                Item_Id = transaction.Item_Id,
                User_Id = transaction.User_Id, // Include User_Id
                Transaction_Type = transaction.Transaction_Type,
                Quantity = transaction.Quantity,
                Transaction_Date = transaction.Transaction_Date,
                Notes = transaction.Notes
            };

            return transactionDto;
        }

        // POST: api/Transaction
        [HttpPost]
        public async Task<ActionResult<TransactionDto>> PostTransaction(TransactionDto transactionDto)
        {
            // Check if the User_Id exists in the Users table
            var userExists = await _context.Users.AnyAsync(u => u.Id == transactionDto.User_Id);
            if (!userExists)
            {
                return BadRequest("User with provided User_Id does not exist.");
            }

            var transaction = new Transaction
            {
                Item_Id = transactionDto.Item_Id,
                User_Id = transactionDto.User_Id, // Assign User_Id
                Transaction_Type = transactionDto.Transaction_Type,
                Quantity = transactionDto.Quantity,
                Transaction_Date = transactionDto.Transaction_Date,
                Notes = transactionDto.Notes
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            transactionDto.Transaction_Id = transaction.Transaction_Id; // Update the DTO with the generated ID

            return CreatedAtAction(nameof(GetTransaction), new { id = transaction.Transaction_Id }, transactionDto);
        }

        // PUT: api/Transaction/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaction(int id, TransactionDto transactionDto)
        {
            if (id != transactionDto.Transaction_Id)
            {
                return BadRequest();
            }

            // Check if the User_Id exists in the Users table
            var userExists = await _context.Users.AnyAsync(u => u.Id == transactionDto.User_Id);
            if (!userExists)
            {
                return BadRequest("User with provided User_Id does not exist.");
            }

            var transaction = new Transaction
            {
                Transaction_Id = transactionDto.Transaction_Id,
                Item_Id = transactionDto.Item_Id,
                User_Id = transactionDto.User_Id, // Assign User_Id
                Transaction_Type = transactionDto.Transaction_Type,
                Quantity = transactionDto.Quantity,
                Transaction_Date = transactionDto.Transaction_Date,
                Notes = transactionDto.Notes
            };

            _context.Entry(transaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Transaction/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.Transaction_Id == id);
        }
    }
}
