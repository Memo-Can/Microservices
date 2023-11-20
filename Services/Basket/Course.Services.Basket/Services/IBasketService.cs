using Course.Services.Basket.Dtos;
using Course.Shared.Dtos;

namespace Course.Services.Basket.Services
{
    public interface IBasketService
    {
        Task<Response<BasketDto>> GetBasketAsync(string userId);
        Task<Response<bool>> SaveOrUpdateAsync(BasketDto basketDto);
        Task<Response<bool>> DeleteAsync(string userId);
    }
}
