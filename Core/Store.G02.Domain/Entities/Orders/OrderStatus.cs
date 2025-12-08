namespace Store.G02.Domain.Entities.Orders
{
    public enum OrderStatus
    {
        //Constatn DataType => Enum
        Pending = 0,
        PaymentSuccess = 1,
        PaymentFailure = 2
    }
}