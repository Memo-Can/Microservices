using Course.Services.Basket.Dtos;
using Course.Services.Basket.Services;
using Course.Shared.Controllers;
using Course.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace Course.Services.Basket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : CustomBaseController
    {
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public BasketController(IBasketService basketService, ISharedIdentityService sharedIdentityService)
        {
            _basketService = basketService; 
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            var claims =User.Claims;

            return CreateActionResultInstance(await _basketService.GetBasketAsync(_sharedIdentityService.GetUserId));
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrUpdateBasket(BasketDto basketDto)
        {
            var response = await _basketService.SaveOrUpdateAsync(basketDto);

            return CreateActionResultInstance(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasket()
        {
            return CreateActionResultInstance(await _basketService.DeleteAsync(_sharedIdentityService.GetUserId));
        }
    }
}
