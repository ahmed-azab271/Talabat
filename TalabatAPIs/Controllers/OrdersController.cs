using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using Talabat.Core.Entites.Order;
using Talabat.Core.IRepositories;
using Talabat.Core.IServices;
using TalabatAPIs.DTOs;
using TalabatAPIs.Errors;

namespace TalabatAPIs.Controllers
{
    public class OrdersController : APIBaseController
    {
        private readonly IOrderService orderService;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public OrdersController(IOrderService _orderService , IMapper _mapper , IUnitOfWork _unitOfWork)
        {
            orderService = _orderService;
            mapper = _mapper;
            unitOfWork = _unitOfWork;
        }
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponce), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Order>> CreateOrder (OrderDto orderDto)
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var Order = await orderService.CreateOrderAsync(BuyerEmail, orderDto.BasketId, orderDto.DelivaryMethodId, orderDto.ShippingAddress );
            if (Order is null) return BadRequest(new ApiResponce(400, "There Is A Problem With Your Order"));
            return Ok(Order);
        }
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(IReadOnlyList<OrderToReturnDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponce), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto?>>> GetOrdersForUser()
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var Orders = await orderService.GetOrdersForUserAsync(BuyerEmail);
            if (Orders is null) return NotFound(new ApiResponce(404, "There Is No Orders For This User"));
            var MappedOrders = mapper.Map <IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(Orders);
            return Ok(MappedOrders);
        }
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponce) , StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderToReturnDto?>> GetOrderByUserId(int id)
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var ordere = await orderService.GetOrdersForUserByIdAsync(BuyerEmail , id);
            if (ordere is null) return NotFound(new ApiResponce(404, $"There Is No Orders for Id = {id}"));
            var MappedOrders = mapper.Map<Order, OrderToReturnDto>(ordere);
            return Ok(MappedOrders);
        }
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetAllDeliveryMethods()
            => Ok( await unitOfWork.CreateRepo<DeliveryMethod>().GetAllAsync());
    }
}
