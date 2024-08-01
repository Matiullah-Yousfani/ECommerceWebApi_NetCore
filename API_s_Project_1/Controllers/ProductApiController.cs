using API_s_Project_1.Data;
using API_s_Project_1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_s_Project_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public ProductApiController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _db.products.Include("Category").ToListAsync();
        }

        [HttpGet("id")]
        public async Task<ActionResult<Product>> GetProduct(int? id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var obj = await _db.products.Include("Category").FirstOrDefaultAsync(x => x.Id == id);
            if (obj == null) 
            {
                return BadRequest();
            }
            return Ok(obj);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product objProduct)
        {
            if (objProduct == null) 
            {
                return BadRequest();
            }
            // here it is checking that the categoryid that comes f rom body does this exist?
            //if yes then this add the category obj with the category field of the body
            var category = await _db.categories.FindAsync(objProduct.CategoryId);
            if (category == null) 
            {
                return BadRequest();
            }
            objProduct.Category = category;
            await _db.products.AddAsync(objProduct);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), new { id = objProduct.Id }, objProduct);
        }

        [HttpPut("id")]
        public async Task<ActionResult<Product>> UpdateProduct(int? id, [FromBody] Product objProduct)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var category = await _db.categories.FindAsync(objProduct.CategoryId);
            if (category == null)
            {
                return BadRequest();
            }
            var product = await _db.products.FindAsync(id);
            if (product == null) 
            {
                return BadRequest();
            }
            product.ProductName = objProduct.ProductName;
            product.Description = objProduct.Description;
            product.Stock = objProduct.Stock;
            product.Price = objProduct.Price;
            product.Price50 = objProduct.Price50;
            product.Price100 = objProduct.Price100;
            product.ListPrice = objProduct.ListPrice;
            product.ProductWeightInKgs = objProduct.ProductWeightInKgs;
            product.imageUrl = objProduct.imageUrl;
            product.CategoryId = objProduct.CategoryId;
            
            _db.products.Update(product);
            await _db.SaveChangesAsync();
            return Ok(product);

        }

        [HttpDelete("id")]
        public async Task<ActionResult<Product>> RemoveProduct(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var obj = await _db.products.FindAsync(id);
            if (obj == null)
            {
                return BadRequest();
            }
            _db.products.Remove(obj);
            await _db.SaveChangesAsync();
            return Ok(obj);
        }
    }
}
