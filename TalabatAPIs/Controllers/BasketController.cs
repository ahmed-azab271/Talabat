using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entites;
using Talabat.Core.IRepositories;
using TalabatAPIs.DTOs;
using TalabatAPIs.Errors;

namespace TalabatAPIs.Controllers
{
    public class BasketController : APIBaseController
    {
        private readonly IBasketRepo basketRepo;
        private readonly IMapper mapper;

        public BasketController(IBasketRepo _basketRepo , IMapper _mapper)
        {
            basketRepo = _basketRepo;
            mapper = _mapper;
        }
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string BasketId)
        {
            var Basket = await basketRepo.GetBasketAsync(BasketId);
            // if null we will ReCreate another Basket With Same BasketId
            if (Basket == null) return new CustomerBasket(BasketId);
            return Ok(Basket);
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto BasketDto)
        {
            // For Validation on Price and Quantity First Before Save it in Database
            var Basket = mapper.Map<CustomerBasketDto, CustomerBasket>(BasketDto);

            var UpdatedorCreatedBasket = await basketRepo.CreateUpdateBasketAsync(Basket);
            if (UpdatedorCreatedBasket == null) return BadRequest(new ApiResponce(400));
            return Ok(UpdatedorCreatedBasket);
        }
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket(string BasketId)
        {
            return await basketRepo.DeleteBasketAsync(BasketId);
        }
    }
}