using System.Runtime.Serialization;

namespace Core.Entities.OrderAggregate
{
    public enum OrderStatus
    {
        // By default in an enum the first item is given the id of 0, the next 1 etc
        // Can specify defaults by using the EnumMember attribute
        
        [EnumMember(Value = "Pending")]
        Pending,
        
        [EnumMember(Value = "Payment Recieved")]
        PaymentReceived,
        
        [EnumMember(Value = "Payment Failed")]
        PaymentFailed

        // In the real world application we would want to know things like:  Shipped, Delivered, returned etc  
    }
}