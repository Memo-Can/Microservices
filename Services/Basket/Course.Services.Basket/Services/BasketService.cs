﻿using Course.Services.Basket.Dtos;
using Course.Shared.Dtos;
using System.Text.Json;

namespace Course.Services.Basket.Services
{
    public class BasketService : IBasketService
    {
        public readonly RedisService _redisService;

        public BasketService(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<Response<BasketDto>> GetBasketAsync(string userId)
        {
            var existBasket = await _redisService.GetDb().StringGetAsync(userId);

            if (string.IsNullOrEmpty(existBasket))
                return Response<BasketDto>.Fail("Basket not found",404);

            //RedisService dataları string olarak tutmaktadır.
            return Response<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(existBasket), 200);
        }

        public async Task<Response<bool>> SaveOrUpdateAsync(BasketDto basketDto)
        {
            var status = await _redisService.GetDb().StringSetAsync(basketDto.UserId, JsonSerializer.Serialize(basketDto));

            return status ? Response<bool>.Success(204) : Response<bool>.Fail("Basket could not update or save", 500);
        }

        public async Task<Response<bool>> DeleteAsync(string userId)
        {
            var status = await _redisService.GetDb().KeyDeleteAsync(userId);

            return status ? Response<bool>.Success(204) : Response<bool>.Fail("Basket not found", 404);
        }
    }
}
