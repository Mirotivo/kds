using System.ComponentModel.DataAnnotations;

public class PurchaseOrder
{
    [Key]
    public int ID { get; set; }
    [MaxLength(255)]
    public string CustomerName { get; set; }
    [MaxLength(255)]
    public string StationGroup { get; set; }
    [MaxLength(255)]
    public string Status { get; set; }
    public DateTime Timestamp { get; set; }
    public string Source { get; set; }
    public List<OrderItem> OrderItems { get; set; }
}