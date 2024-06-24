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
    public class InventoryController : ControllerBase
    {
        private readonly DbContextClass _context;

        public InventoryController(DbContextClass context)
        {
            _context = context;
        }

        // GET: api/Inventory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryDto>>> GetInventory()
        {
            var inventoryItems = await _context.Inventory
                .Select(inventory => new InventoryDto
                {
                    Inventory_Id = inventory.Inventory_Id,
                    Item_Id = inventory.Item_Id,
                    Quantity = inventory.Quantity,
                    Location = inventory.Location,
                    Last_Updated = inventory.Last_Updated
                })
                .ToListAsync();

            return inventoryItems;
        }

        // GET: api/Inventory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryDto>> GetInventory(int id)
        {
            var inventory = await _context.Inventory.FindAsync(id);

            if (inventory == null)
            {
                return NotFound();
            }

            var inventoryDto = new InventoryDto
            {
                Inventory_Id = inventory.Inventory_Id,
                Item_Id = inventory.Item_Id,
                Quantity = inventory.Quantity,
                Location = inventory.Location,
                Last_Updated = inventory.Last_Updated
            };

            return inventoryDto;
        }

        // POST: api/Inventory
        [HttpPost]
        public async Task<ActionResult<InventoryDto>> PostInventory(InventoryDto inventoryDto)
        {
            var inventory = new Inventory
            {
                Item_Id = inventoryDto.Item_Id,
                Quantity = inventoryDto.Quantity,
                Location = inventoryDto.Location,
                Last_Updated = inventoryDto.Last_Updated
            };

            _context.Inventory.Add(inventory);
            await _context.SaveChangesAsync();

            inventoryDto.Inventory_Id = inventory.Inventory_Id; // Update the DTO with the generated ID

            return CreatedAtAction(nameof(GetInventory), new { id = inventory.Inventory_Id }, inventoryDto);
        }

        // PUT: api/Inventory/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventory(int id, InventoryDto inventoryDto)
        {
            if (id != inventoryDto.Inventory_Id)
            {
                return BadRequest();
            }

            var inventory = new Inventory
            {
                Inventory_Id = inventoryDto.Inventory_Id,
                Item_Id = inventoryDto.Item_Id,
                Quantity = inventoryDto.Quantity,
                Location = inventoryDto.Location,
                Last_Updated = inventoryDto.Last_Updated
            };

            _context.Entry(inventory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryExists(id))
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

        // DELETE: api/Inventory/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventory(int id)
        {
            var inventory = await _context.Inventory.FindAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }

            _context.Inventory.Remove(inventory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InventoryExists(int id)
        {
            return _context.Inventory.Any(e => e.Inventory_Id == id);
        }
    }
}
