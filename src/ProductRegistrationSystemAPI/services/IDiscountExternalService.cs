namespace ProductRegistrationSystemAPI.services
{
    public interface IDiscountExternalService
    {
        Task<int> GetWithoutDiscount();
        Task<int> GetDiscount();
    }
}
