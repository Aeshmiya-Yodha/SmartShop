
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("Orders")]
public class OrderController : ControllerBase
{
    private readonly AppDbContext _context;
    public OrderController(AppDbContext context)
    {
        _context = context;
    }
    [HttpGet("User/{UserId}")]
    public async Task<ActionResult<List<Order>>> GetOrderDetailsByUserId([FromRoute] int UserId)
    {
        List<Order> UserOrders = await _context.Orders.Where(O => O.UserId == UserId).ToListAsync();
        if (UserOrders.Count == 0)
        {
            return BadRequest("No orders found for this user");
        }
        return Ok(UserOrders);
    }

    [HttpGet("{OrderId}")]
    public async Task<ActionResult> GetOrdersDetailsByOrderId([FromRoute] int OrderId)
    {
        var OrderDetails = await _context.Orders.Where(O => O.OrderId == OrderId).FirstOrDefaultAsync();
        if (OrderDetails == null)
        {
            return BadRequest("No Order with such Id exist");
        }
        return Ok(OrderDetails);
    }
    [HttpPost]
    public async Task<ActionResult> AddOrderDetails([FromBody] Order OrderDetails)
    {
        var ExistingOrder = await _context.Orders.Where(O => O.OrderId == OrderDetails.OrderId).FirstOrDefaultAsync();
        if (ExistingOrder != null)
        {
            return BadRequest("Order with the OrderId already exist");
        }
        else
        {
            decimal total = 0;
            //Cant trust the Client side calculation 
            foreach (var item in OrderDetails.OrderItems)
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == item.ProductId);
                if (product == null)
                {
                    return BadRequest($"Product {item.ProductId} does not exist");
                }

                item.PriceWhenOrdered = product.Price;   // snapshot server-side price
                total += item.PriceWhenOrdered * item.Quantity;
            }

            OrderDetails.TotalAmount = total;

            // Server controls these at creation; never trust the client
            OrderDetails.Status = OrderStatus.Pending;
            OrderDetails.isPaid = false;
            OrderDetails.OrderDate = DateTime.UtcNow;

            await _context.Orders.AddAsync(OrderDetails);
            await _context.SaveChangesAsync();
            return Ok("Order Added Successfully");
        }
    }
    [HttpPut("OrderStatus/{OrderId}")]
    public async Task<ActionResult> UpdateOrderStatus([FromRoute] int OrderId, [FromBody] OrderStatus orderStatus)
    {
        var ExistingOrder = await _context.Orders.Where(O => O.OrderId == OrderId).FirstOrDefaultAsync();
        if (ExistingOrder == null)
        {
            return BadRequest("Order with the OrderId does not exist");
        }
        else
        {
            ExistingOrder.Status = orderStatus;
            await _context.SaveChangesAsync();
            return Ok("Order status updated successfully!");
        }
    }

    [HttpPut("PaymentStatus/{OrderId}")]
    public async Task<ActionResult> UpdatePaymentStatus([FromRoute] int OrderId, [FromQuery] bool isPaid)
    {
        var ExistingOrder = await _context.Orders.FirstOrDefaultAsync(O => O.OrderId == OrderId);
        if (ExistingOrder == null)
        {
            return BadRequest("Order with the OrderId does not exist");
        }

        // Optional rule: a cancelled order cannot be marked as paid
        if (ExistingOrder.Status == OrderStatus.Cancelled && isPaid)
        {
            return BadRequest("A cancelled order cannot be marked as paid");
        }

        ExistingOrder.isPaid = isPaid;
        await _context.SaveChangesAsync();
        return Ok($"Order payment status updated to {(isPaid ? "Paid" : "Unpaid")}");
    }
    


}