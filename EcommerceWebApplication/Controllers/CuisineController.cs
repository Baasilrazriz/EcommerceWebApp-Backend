﻿using EcommerceWebApplication.Data;
using EcommerceWebApplication.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;
using System.Reflection.Metadata;

namespace EcommerceWebApplication.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CuisineController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CreateCuisines createCuisines;
        public CuisineController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetCuisines()
        {
            var cuisines = _context.CuisineModels.ToListAsync();
            if (cuisines != null)
            {
                return Ok(cuisines);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCuisines([FromBody] CuisineDto cuisineDto)
        {
            if (cuisineDto == null)
            {
                return NoContent();
            }
            var create = await createCuisines.CreateCuisinesAsync(cuisineDto);
            if (create != null)
            {
                return Ok(create);
            }
            return BadRequest("Error occured");
        }
        [HttpDelete("{id}")]
        public async Task <IActionResult> DeleteCuisine (int id)
        {
            var cuisine = await _context.CuisineModels.FindAsync(id);
            if (cuisine != null)
            {
                _context.Remove(cuisine);
                await _context.SaveChangesAsync();
            }
            return NoContent();
        }


    }
}