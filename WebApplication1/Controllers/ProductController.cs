using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Model;
using WebApplication1.Model.Data.EfCore;
using WebApplication1.Model.DTO;

namespace WebApplication1.Controllers
{   [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {   
       private readonly SocialContext  _context ;

        public ProductController(SocialContext context)
        {
            _context = context;
          
        }
        [AllowAnonymous]
        public async Task<IActionResult> GetProducts()
        {
            var product = await _context.Products
                .Select(x => new ProductDto { Name = x.Name, isActive = x.isActive, Price = x.Price, ProductID = x.ProductID })
                .ToListAsync();

            return Ok(product);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var p = await _context.Products
                .Select(x => new ProductDto { Name = x.Name, isActive = x.isActive, Price = x.Price, ProductID = x.ProductID })
                .FirstOrDefaultAsync(i => i.ProductID == id);
            if (p == null)
            {
                return NotFound();
            }
            return Ok(p);
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product p)
        {
            _context.Products.Add(p);
           await _context.SaveChangesAsync();
            return  CreatedAtAction(nameof(GetProduct),new { id=p.ProductID},p);
        }
         [HttpPut("{id}")] 

        public async Task<IActionResult> UpdateProduct(int id, Product entity)
        {
            if (id != entity.ProductID)
            {
                return BadRequest();
            }
            var product= await _context.Products.FindAsync(id);

            if (product!= null)
            {
                product.Name = entity.Name;
                product.Price = entity.Price;

                try
                {
                    await _context.SaveChangesAsync();
                    
                }
                catch(Exception e)
                {
                    return NotFound();
                }
                return NoContent();
            }
            return NotFound();
        }
        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(i => i.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
