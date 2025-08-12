namespace webecommerce.Common.Enums
{
    public enum StatusOrderEnum
    {
        Pending = 0,
        Processing = 1,
        Shipped = 2,
        Delivered = 3,
        Cancelled = 4,
        Returned = 5
    }

    public enum PaymentMethodEnum
    {
        Cash = 0,
        CreditCard = 1,
        BankTransfer = 2,
        EWallet = 3
    }

    public enum PaymentStatusEnum
    {
        Pending = 0,
        Paid = 1,
        Failed = 2,
        Refunded = 3
    }
} 