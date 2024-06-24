using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Laboratory_Management_API.DataConnection;
using Laboratory_Management_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Laboratory_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly DbContextClass _context;

        public ItemController(DbContextClass context)
        {
            _context = context;
        }

        // GET: api/Item
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetItems()
        {
            var items = await _context.Items
                .Select(item => new ItemDto
                {
                    Item_Id = item.Item_Id,
                    Item_Name = item.Item_Name,
                    Description = item.Description,
                    Category_Id = item.Category_Id,
                    Created_At = item.Created_At
                })
                .ToListAsync();

            return items;
        }

        // GET: api/Item/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItem(int id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            var itemDto = new ItemDto
            {
                Item_Id = item.Item_Id,
                Item_Name = item.Item_Name,
                Description = item.Description,
                Category_Id = item.Category_Id,
                Created_At = item.Created_At
            };

            return itemDto;
        }

        // POST: api/Item
        [HttpPost]
        public async Task<ActionResult<ItemDto>> PostItem(ItemDto itemDto)
        {
            var item = new Item
            {
                Item_Name = itemDto.Item_Name,
                Description = itemDto.Description,
                Category_Id = itemDto.Category_Id,
                Created_At = itemDto.Created_At
            };

            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            itemDto.Item_Id = item.Item_Id; // Update the DTO with the generated ID

            return CreatedAtAction(nameof(GetItem), new { id = item.Item_Id }, itemDto);
        }

        // PUT: api/Item/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(int id, ItemDto itemDto)
        {
            if (id != itemDto.Item_Id)
            {
                return BadRequest();
            }

            var item = new Item
            {
                Item_Id = itemDto.Item_Id,
                Item_Name = itemDto.Item_Name,
                Description = itemDto.Description,
                Category_Id = itemDto.Category_Id,
                Created_At = itemDto.Created_At
            };

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
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

        // DELETE: api/Item/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.Item_Id == id);
        }
    }
}
