using BurguerManiaAPI.status;

namespace BurguerManiaAPI.Orders;

public class Order
{
    public int Id { get; init; } 
    public int StatusId { get; private set; } 
    public decimal Value { get; private set; } 

    public Status? Status { get; private set; } 

    public Order(int statusId, decimal value)
    {
        StatusId = statusId;
        Value = value;
    }

    public void UpdateStatus(int statusId)
    {
        StatusId = statusId;
    }

    public void UpdateValue(decimal value)
    {
        if (value < 0)
            throw new ArgumentException("Valor não pode ser negativo!", nameof(value));
        Value = value;
    }
}
