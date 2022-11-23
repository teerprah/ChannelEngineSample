namespace Shared.Enums;

public class OrderStatus: Enumeration
{
    private OrderStatus(int id, string name) : base(id, name)
    {
    }
    
    public static OrderStatus InProgress => new(1, "IN_PROGRESS");
    public static OrderStatus Shipped => new(1, "SHIPPED");
    public static OrderStatus InBackorder => new(1, "IN_BACKORDER");
    public static OrderStatus Manco => new(1, "MANCO");
    public static OrderStatus Cancelled => new(1, "CANCELED");
    public static OrderStatus InCombi => new(1, "IN_COMBI");
    public static OrderStatus Closed => new(1, "CLOSED");
    public static OrderStatus New => new(1, "NEW");
    public static OrderStatus Returned => new(1, "RETURNED");
    public static OrderStatus RequiresCorrection => new(1, "REQUIRES_CORRECTION");
    public static OrderStatus AwaitingPayment => new(1, "AWAITING_PAYMENT");
}