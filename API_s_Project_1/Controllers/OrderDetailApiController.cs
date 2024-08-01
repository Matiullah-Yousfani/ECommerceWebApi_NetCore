using API_s_Project_1.Data;
using API_s_Project_1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace API_s_Project_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailApiController : ControllerBase
    {
        public readonly ApplicationDbContext _db;

        public OrderDetailApiController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("id")]
        public async Task<ActionResult<OrderDetail>> GetOrderDetail(int id)
        {
            var orderObj = await _db.orderDetails.FindAsync(id);
            return Ok(orderObj);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDetail>> CreateOrderDetail([FromBody]OrderDetail orderDetail)
        {
            if (orderDetail == null)
            {
                return BadRequest();
            }
            var orderHeader = await _db.orderHeaders.FindAsync(orderDetail.OrderId);
            if (orderHeader == null)
            {
                return BadRequest();
            }
            orderDetail.OrderHeader = orderHeader;
            var product = await _db.products.FindAsync(orderDetail.ProductId);
            orderDetail.Product = product;
            _db.orderDetails.Add(orderDetail);
            await _db.SaveChangesAsync();
            return Ok(orderDetail);    
        }

        [HttpPut("id")]
        public async Task<ActionResult<OrderDetail>> UpdateOrderDetail(int id, [FromBody]OrderDetail orderdetail)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var orderHeader = await _db.orderHeaders.FindAsync(orderdetail.OrderId);
            if (orderHeader == null)
            {
                return BadRequest();
            }
            var product = await _db.products.FindAsync(orderdetail.ProductId);
            var order = await _db.orderDetails.FindAsync(id);
            order.Count = orderdetail.Count;
            order.Price = orderdetail.Price;
            order.OrderHeader = orderHeader;
            order.Product = product;

            _db.orderDetails.Update(order);
            await _db.SaveChangesAsync();  
            return Ok(order);

        }

        [HttpDelete("id")]
        public async Task<ActionResult<OrderDetail>> RemoveOrder(int id)
        {
            var orderObj = await _db.orderDetails.FindAsync(id);
            _db.orderDetails.Remove(orderObj);
            await _db.SaveChangesAsync();
            return Ok(orderObj);
        }

    }
}
