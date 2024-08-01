using API_s_Project_1.Data;
using API_s_Project_1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_s_Project_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderHeaderApiController : ControllerBase
    {
        public readonly ApplicationDbContext _db;
        public OrderHeaderApiController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("id")]
        public async Task<ActionResult<IEnumerable<OrderHeader>>> GetOrder(int id)
        {
            var order = await _db.orderHeaders.FirstOrDefaultAsync(u => u.Id == id);
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<OrderHeader>> CreateOrder([FromBody] OrderHeader order)
        {
            if (order == null)
            {
                return BadRequest();
            }
            var user = await _db.Users.FindAsync(order.ApplicationUserId);
            if (user == null)
            {
                return BadRequest();
            }
            order.ApplicationUser = user;
            _db.orderHeaders.Add(order);
            await _db.SaveChangesAsync();
            return Ok(order);
        }

        [HttpPut("id")]
        public async Task<ActionResult<OrderHeader>> UpdateHeader(int id, [FromBody] OrderHeader orderHeader)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var order = await _db.orderHeaders.FindAsync(id);
            if (order == null)
            {
                return BadRequest();
            }
            order.ApplicationUserId = orderHeader.ApplicationUserId;
            order.Name = orderHeader.Name;
            order.PhoneNumber = orderHeader.PhoneNumber;
            order.City = orderHeader.City;
            order.State = orderHeader.State;
            order.StreetAddress = orderHeader.StreetAddress;
            order.PostalCode = orderHeader.PostalCode;
            order.OrderDate = orderHeader.OrderDate;
            order.ShippingDate = orderHeader.ShippingDate;
            order.OrderTotal = orderHeader.OrderTotal;
            order.OrderStatus = orderHeader.OrderStatus;
            order.PaymentStatus = orderHeader.PaymentStatus;
            order.TrackingNumber = orderHeader.TrackingNumber;
            order.Carrier = orderHeader.Carrier;
            order.PaymentDate = orderHeader.PaymentDate;
            order.PaymentDueDate = orderHeader.PaymentDueDate;
            order.SessionId = orderHeader.SessionId;
            order.PaymentIntentId = orderHeader.PaymentIntentId;
            _db.orderHeaders.Update(order);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("id")]
        public async Task<ActionResult<IEnumerable<OrderHeader>>> RemoveOrderHeader(int id)
        {
            var order = await _db.orderHeaders.FirstOrDefaultAsync(u => u.Id == id);
            _db.orderHeaders.Remove(order);
            await _db.SaveChangesAsync();
            return Ok(order);
        }

    }
}
