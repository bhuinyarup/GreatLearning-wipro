namespace EcommerceApp.DTOs
{
    public record CreateProductDto(string Name, decimal Price, string Description, int StockQuantity, string Category = "General");
    public record CreateCustomerDto(string Name, string Email, string Password);
    public record AddToCartDto(int CustomerId, int ProductId, int Quantity);
    public record RemoveFromCartDto(int CustomerId, int ProductId);
    public record PlaceOrderDto(int CustomerId, string ShippingAddress, List<OrderItemDto> Items);
    public record OrderItemDto(int ProductId, int Quantity);

    public record ApiResponse<T>(bool Success, string Message, T? Data);
}
