using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entites;
using Talabat.Core.IRepositories;
using Talabat.Core.Specification;
using TalabatAPIs.DTOs;
using TalabatAPIs.Errors;
using TalabatAPIs.Helpers;

namespace TalabatAPIs.Controllers
{
    public class ProductsController : APIBaseController
    {
        private readonly IGenaricRepo<Product> productRepo;
        private readonly IGenaricRepo<ProductType> typeRepo;
        private readonly IGenaricRepo<ProductBrand> brandRepo;
        private readonly IMapper mapper;

        public ProductsController(IGenaricRepo<Product> _productRepo ,IGenaricRepo<ProductType> _typeRepo , IGenaricRepo<ProductBrand> _brandRepo , IMapper _mapper)
        {
            productRepo = _productRepo;
            typeRepo = _typeRepo;
            brandRepo = _brandRepo;
            mapper = _mapper;
        }

        [CacheAttribute(3600)]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery] ProductSpecParms Parms )
        {
            var Spec = new ProductWithBrandWithTypeSpecification(Parms);
            var Products = await productRepo.GetAllWithSpecAsync(Spec);
            var CountSpec = new ProductsWithFilterationForCount(Parms);
            var ProductsCount = await productRepo.GetCountWithSpecAsync(CountSpec);
            var ProductsDTO = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDTO>>(Products);
            var ReturnPaginationDTO = new Pagination<ProductToReturnDTO>()
            {
                PageIndex = Parms.PageIndex,
                PageSize = Parms.PageSize,
                Count = ProductsCount,
                Data = ProductsDTO,
            };
            return Ok(ReturnPaginationDTO);
        }

        [HttpGet("{id}")]
        [CacheAttribute(3600)]
        [ProducesResponseType(typeof(ProductToReturnDTO) , StatusCodes.Status200OK)] //Default (already exict)
        [ProducesResponseType(typeof(ApiResponce) , StatusCodes.Status404NotFound)] //Added 
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var Spec = new ProductWithBrandWithTypeSpecification(id);
            var product = await productRepo.GetByIdWithSpecAsync(Spec);
            if (product == null)
                return NotFound(new ApiResponce(404));
            var ProductDTO = mapper.Map<Product, ProductToReturnDTO>(product);
            return Ok(ProductDTO);
        }

        [CacheAttribute(3600)]
        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetAllProductTypes()
            => Ok(await typeRepo.GetAllAsync());

        [CacheAttribute(3600)]
        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllProductBrands()
            => Ok(await brandRepo.GetAllAsync());
    }
}
