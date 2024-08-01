using API_s_Project_1.Data;
using API_s_Project_1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DiaSymReader;

namespace API_s_Project_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartApiController : ControllerBase
    {
        public readonly ApplicationDbContext _db;
        public CartApiController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("id")]
        public async Task<ActionResult<IEnumerable<ShoppingCart>>> cartList(string id)
        {
            var cartList = _db.shoppingCarts.FirstOrDefault(x => x.ApplicationUserId == id);
            return Ok(cartList);
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> AddItem([FromBody] ShoppingCart obj)
        {
            var Product = await _db.products.FindAsync(obj.ProductId);
            if (Product == null)
            {
                return BadRequest();
            }
            obj.Product = Product;
            var users = await _db.Users.FindAsync(obj.ApplicationUserId);
            if (users == null)
            {
                return BadRequest();
            }
            obj.ApplicationUser = users;
            _db.shoppingCarts.Add(obj);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(cartList), new { id = obj.ApplicationUserId }, obj);
        }

        [HttpPut("id")]
        public async Task<ActionResult<ShoppingCart>> UpdateCart(int cartId, [FromBody] ShoppingCart obj)
        {
            var cart = await _db.shoppingCarts.FindAsync(cartId);
            cart.ProductId = obj.ProductId;
            cart.ApplicationUserId = obj.ApplicationUserId;
            cart.count = obj.count;
            _db.shoppingCarts.Update(cart);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("id")]
        public async Task<ActionResult<ShoppingCart>> RemoveItem(int id)
        {
            var item = await _db.shoppingCarts.FindAsync(id);
            _db.shoppingCarts.Remove(item);
            await _db.SaveChangesAsync();
            return Ok();

        }
    }
}
