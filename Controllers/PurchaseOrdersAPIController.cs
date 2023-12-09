using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using kds.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace kds.Controllers;

[Route("api/purchaseorders")]
[ApiController]
public class PurchaseOrdersAPIController : ControllerBase
{
    private readonly IPurchaseOrderService _purchaseOrderService;

    public PurchaseOrdersAPIController(IPurchaseOrderService purchaseOrderService)
    {
        _purchaseOrderService = purchaseOrderService;
    }

    [HttpGet]
    public List<PurchaseOrderDto> GetPurchaseOrders()
    {
        var pos = _purchaseOrderService.GetPurchaseOrders();
        var posDto = pos.Select(po => ObjectMapper.MapToDto<PurchaseOrder, PurchaseOrderDto>(po)).ToList();
        return posDto;
    }

    [HttpPost]
    public IActionResult CreatePurchaseOrder([FromBody] PurchaseOrderDto purchaseOrderDto)
    {
        if (purchaseOrderDto == null)
        {
            return BadRequest("Invalid purchase order data");
        }

        // Map the PurchaseOrderDto to your PurchaseOrder entity
        var purchaseOrder = new PurchaseOrder
        {
            CustomerName = purchaseOrderDto.CustomerName,
            StationGroupID = purchaseOrderDto.StationGroupID,
            Status = purchaseOrderDto.Status,
            Timestamp = DateTime.UtcNow,
            Source = purchaseOrderDto.Source,
            OrderItems = purchaseOrderDto.OrderItems
                .Select(orderItemDto => new OrderItem
                {
                    Title = orderItemDto.Title,
                    Description = orderItemDto.Description,
                    Price = orderItemDto.Price,
                    Quantity = orderItemDto.Quantity,
                    CategoryID = orderItemDto.CategoryID
                })
                .ToList()
        };

        // Save the purchase order to the database using Entity Framework Core
        _purchaseOrderService.CreatePurchaseOrder(purchaseOrder);

        // You can return the created purchase order or an appropriate success message
        var createdPurchaseOrderDto = new PurchaseOrderDto
        {
            ID = purchaseOrder.ID,
            CustomerName = purchaseOrder.CustomerName,
            StationGroupID = purchaseOrder.StationGroupID,
            Status = purchaseOrder.Status,
            Timestamp = purchaseOrder.Timestamp,
            Source = purchaseOrder.Source,
            OrderItems = purchaseOrder.OrderItems
                .Select(orderItem => new OrderItemDto
                {
                    Title = orderItem.Title,
                    Description = orderItem.Description,
                    Price = orderItem.Price,
                    Quantity = orderItem.Quantity,
                    CategoryID = orderItem.CategoryID
                })
                .ToList()
        };

        return Ok(createdPurchaseOrderDto);
    }
}
