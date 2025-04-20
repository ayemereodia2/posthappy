using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using blog_core.Dto;
using blog_core.Dto.RequestDto;
using blog_core.Dto.ResponseDto;
using blog_core.entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace my_blog_service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;
        private readonly TokenService _tokenService;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            ILogger<AuthController> logger,
            TokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
            _logger = logger;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegisterResponseDto>> Register([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Where(ms => ms.Value.Errors.Count > 0)
                                        .SelectMany(x => x.Value.Errors
                                        .Select(ms => new ErrorDto
                                        {
                                            Field = x.Key,
                                            Message = ms.ErrorMessage
                                        })).ToList();

                return BadRequest(new RegisterResponseDto
                {
                    Success = false,
                    Message = "Validation Failed",
                    Errors = errors
                });
            }

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };

            try
            {
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var response = new RegisterResponseDto
                    {
                        Success = true,
                        Message = "User registered successfully",
                        Email = user.Email
                    };
                    return CreatedAtAction(nameof(Register), response);
                }

                var errorMessage = string.Join("; ", result.Errors.Select(e => e.Description));
                // log errorMessage
                _logger.LogError(errorMessage);

                var errorResponse = new RegisterResponseDto
                {
                    Success = false,
                    Message = $"User registration failed with errors {errorMessage}"
                };

                return BadRequest(errorResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.InnerException.Message);

                return StatusCode(500, new RegisterResponseDto
                {
                    Success = false,
                    Message = "Internal Server Error"
                });
            }
        }


        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Where(ms => ms.Value.Errors.Count > 0)
                                        .SelectMany(x => x.Value.Errors
                                        .Select(ms => new ErrorDto
                                        {
                                            Field = x.Key,
                                            Message = ms.ErrorMessage
                                        })).ToList();

                return BadRequest(new RegisterResponseDto
                {
                    Success = false,
                    Message = "Validation Failed",
                    Errors = errors
                });
            }

            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    var token = _tokenService.GenerateToken(user);
                    return CreatedAtAction(nameof(Login), new LoginResponseDto
                    {
                        Success = true,
                        Message = "Success",
                        Token = token
                    });
                }
                else
                {
                    return Unauthorized(new LoginResponseDto
                    {
                        Success = false,
                        Message = "Login Failed: Unauthorized"
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, new LoginResponseDto
                {
                    Success = false,
                    Message = "Internal Server Error"
                });
            }
        }
    }
}