
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

[ApiController]
[Route("Orders")]
public class OrderController :ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetOrders()
    {
        await Task.Delay(100);
        return Ok("All Orders Returned Successfully");
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetOrdersbyId (int id)
    {
        await Task.Delay(100);
        return Ok("Order with the Id returned Successfully");
    }

    
}