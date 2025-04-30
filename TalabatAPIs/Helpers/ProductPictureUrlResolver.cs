using AutoMapper;
using AutoMapper.Execution;
using Talabat.Core.Entites;
using TalabatAPIs.DTOs;

namespace TalabatAPIs.Helpers
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDTO, string>
    {
        private readonly IConfiguration configuration;

        public ProductPictureUrlResolver(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public string Resolve(Product source, ProductToReturnDTO destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl))
                return $"{configuration["ApiBaseUrl"]}{source.PictureUrl}"; // {https://localhost:7206/}{images/products/image.png}
            return string.Empty;
        }
    }
}
