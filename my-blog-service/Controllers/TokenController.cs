using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace my_blog_service.Controllers
{

[Route("token")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly TokenService _tokenService;

    public TokenController(TokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost]
    public async Task<IActionResult> GetToken()
    {
        var form = await Request.ReadFormAsync();
        var clientId = form["client_id"].ToString();
        var clientSecret = form["client_secret"].ToString();
        var grantType = form["grant_type"].ToString();

        if (grantType != "client_credentials" || string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret))
        {
            return BadRequest("Invalid request");
        }

        if (!_tokenService.ValidateClient(clientId, clientSecret))
        {
            return Unauthorized();
        }

        var token = _tokenService.GenerateToken(clientId);
        return Ok(new
        {
            access_token = token,
            token_type = "Bearer",
            expires_in = 3600
        });
    }
}
}