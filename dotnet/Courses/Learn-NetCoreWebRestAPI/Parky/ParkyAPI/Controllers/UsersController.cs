﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkyAPI.Models;
using ParkyAPI.Repository.IRepository;

namespace ParkyAPI.Controllers;

[Authorize]
[Route("api/v{version:apiVersion}/Users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepo;

    public UsersController(IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public IActionResult Authenticate([FromBody] AuthenticationModel model)
    {
        var userDto = _userRepo.Authenticate(model.Username, model.Password).AsDto();

        if (userDto == null)
        {
            return BadRequest(new {message = "Username or password is incorrect"});
        }

        return Ok(userDto);

    }

    [AllowAnonymous]
    [HttpPost("register")]
    public IActionResult Register([FromBody] AuthenticationModel model)
    {
        bool ifUserNameUnique = _userRepo.IsUniqueUser(model.Username);
        if (!ifUserNameUnique)
        {
            return BadRequest(new {message = "Username already exists"});
        }

        var user = _userRepo.Register(model.Username, model.Password);
        if (user == null)
        {
            return BadRequest(new {message = "Error while registering"});
        }

        return Ok();

    }

}