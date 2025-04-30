using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.Core.Entites.Identity;
using Talabat.Core.IServices;
using TalabatAPIs.DTOs;
using TalabatAPIs.Errors;
using TalabatAPIs.Exctensions;

namespace TalabatAPIs.Controllers
{
    public class AccountController : APIBaseController
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ITokenService tokenService;
        private readonly IMapper mapper;

        public AccountController(UserManager<AppUser> _userManager , SignInManager<AppUser> _signInManager , ITokenService _tokenService , IMapper _mapper)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            tokenService = _tokenService;
            mapper = _mapper;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            if (CheckEmailexists(model.Email).Result.Value)
                return BadRequest(new ApiResponce(400, "The Email is Taken"));
            var User = new AppUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                PhoneNumber = model.PhoneNumber,
            };
            var Result = await userManager.CreateAsync(User , model.Password);
            if (Result.Succeeded)
            {
                var ReturndUserDto = new UserDto()
                {
                    DisplayName = User.DisplayName,
                    Email = User.Email,
                    Token = await tokenService.CreateTokenAsync(User, userManager)
                };
                return Ok(ReturndUserDto);
            }
            else { return BadRequest(new ApiResponce(400)); }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto login)
        {
            var User = await userManager.FindByEmailAsync(login.Email);
            if (User == null) return Unauthorized(new ApiResponce(401));
            var Result = await signInManager.CheckPasswordSignInAsync(User, login.Password, false);
            if (Result.Succeeded)
            {
                var ReturndUserDto = new UserDto()
                {
                    DisplayName = User.DisplayName,
                    Email = User.Email,
                    Token = await tokenService.CreateTokenAsync(User, userManager)
                };
                return Ok(ReturndUserDto);
            }
            else { return Unauthorized(new ApiResponce(401)); }
        }

        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            // To find the Email of the user that Logged in RN
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.FindByEmailAsync(Email);
            var ReturnedUserDto = new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = Email,
                Token = await tokenService.CreateTokenAsync(user, userManager)
            };
            return Ok(ReturnedUserDto);
        }

        [Authorize]
        [HttpGet("GetAddress")]
        public async Task<ActionResult<AddressDto>> GetAddress()
        {
            var user =await userManager.FindUserIncludedWithAddressAsync(User);
            var ReturndAddressDto = mapper.Map<Address,AddressDto>(user.Address);
            return Ok(ReturndAddressDto);
        }

        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> GetAddress(AddressDto UpdatedaddressDto)
        {
            var user = await userManager.FindUserIncludedWithAddressAsync(User);
            var Address = mapper.Map<AddressDto, Address>(UpdatedaddressDto);
            if( user.Address != null )
                Address.Id = user.Address.Id;
            else Address.Id = 0;
            user.Address = Address;
            var Result = await userManager.UpdateAsync(user);
            if(!Result.Succeeded) return BadRequest(new ApiResponce(400));
            var MappedAddress = mapper.Map<Address,AddressDto>(Address);
            return Ok(MappedAddress);
        }

        [HttpGet("CheckEmailexists")]
        public async Task<ActionResult<bool>> CheckEmailexists(string Email)
        {
            return await userManager.FindByEmailAsync(Email) is null ? false : true;
        }
    }
}
