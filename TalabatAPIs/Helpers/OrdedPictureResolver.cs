using AutoMapper;
using Talabat.Core.Entites.Order;
using TalabatAPIs.DTOs;

namespace TalabatAPIs.Helpers
{
    public class OrdedPictureResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration configuration;

        public OrdedPictureResolver(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{configuration["ApiBaseUrl"]}{source.PictureUrl}";
            return string.Empty;
        }
    }
}
