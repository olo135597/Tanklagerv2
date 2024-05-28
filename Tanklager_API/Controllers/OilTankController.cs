using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Web.Http.Description;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tanklager_API.Models;
using TanklagerLibraryv2;

namespace Tanklager_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OilTankController : ControllerBase
    {
        private TankStock _tankStock;
        private readonly TanklagerContext _context;

        public OilTankController(TanklagerContext context)
        {
            
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OilTank>>> GetOilTanks()
        {
            return await _context.OilTanks.ToListAsync();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<OilTank>> GetOilTank(int id)
        {
            var oilTank = await _context.OilTanks.FindAsync(id);

            if (oilTank == null)
            {
                return NotFound();
            }

            return oilTank;
        }

      
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOilTank(int id, OilTank oilTank)
        {
            if (id != oilTank.id)
            {
                return BadRequest();
            }

            _context.Entry(oilTank).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OilTankExists(id))
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

        [ResponseType(typeof(OilTank))]
        [HttpPost]
        public async Task<ActionResult<OilTankDTO>> PostOilTank(OilTankDTO oilTankDTO)
        {
          
            var tank = new OilTank()
            {
                id = oilTankDTO.id,
                name = oilTankDTO.name,
                capacity = oilTankDTO.capacity,
                
            };
            _context.OilTanks.Add(tank);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOilTank", new { id = tank.id }, oilTankDTO);
        }

      
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOilTank(int id)
        {
            var oilTank = await _context.OilTanks.FindAsync(id);
            if (oilTank == null)
            {
                return NotFound();
            }

            _context.OilTanks.Remove(oilTank);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OilTankExists(int id)
        {
            return _context.OilTanks.Any(e => e.id == id);
        }
    }
}
