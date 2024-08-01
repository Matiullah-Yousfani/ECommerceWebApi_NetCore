using API_s_Project_1.Data;
using API_s_Project_1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_s_Project_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryApiController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public CategoryApiController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await _db.categories.ToListAsync();
        }

        [HttpGet("id")]
        public async Task<ActionResult<Category>> GetCategory(int? id)
        {
            var category = await _db.categories.FindAsync(id);
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory([FromBody] Category categoryObj)
        {
            if (categoryObj == null)
            {
                return BadRequest();
            }
            await _db.categories.AddAsync(categoryObj);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCategory), new { id = categoryObj.Id }, categoryObj);
        }

        [HttpPut("id")]
        public async Task<ActionResult<Category>> UpdateCategory(int? id, [FromBody] Category categoryObj)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var obj = await _db.categories.FindAsync(categoryObj.Id);
            obj.CategoryName = categoryObj.CategoryName;
            obj.CategoryDescription = categoryObj.CategoryDescription;
            _db.categories.Update(obj);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<ActionResult<Category>> RemoveCategory(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var obj = await _db.categories.FindAsync(id);
            _db.categories.Remove(obj);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
