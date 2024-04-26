using Microsoft.AspNetCore.Mvc;
using TheEasyBazar.Api.Models;
using TheEasyBazar.Service.DTOs.Users;
using TheEasyBazar.Service.Interfaces;

namespace TheEasyBazar.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;

    public UsersController(IUserService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(new Response
        {
            Code = 200,
            Message = "success",
            Data = await _service.RetrieveAllAsync()
        });


    [HttpGet("id")]
    public async Task<IActionResult> GetById(long id)
        => Ok(new Response
        {
            Code = 200,
            Message = "success",
            Data = await _service.RetrieveByIdAsync(id)
        });

    [HttpPost]
    public async Task<IActionResult> Create(UserForCreationDto dto)
        => Ok( new Response
        {
            Code = 200,
            Message = "success",
            Data = await _service.CreateAsync(dto)
        });

    [HttpDelete("id")]
    public async Task<IActionResult> Delete(long id)
        => Ok(new Response
        {
            Code = 200,
            Message = "success",
            Data = await _service.RemoveAsync(id)
        });

    [HttpPut]
    public async Task<IActionResult> Update(UserForUpdateDto dto)
        => Ok(new Response
        {
            Code = 200,
            Message = "success",
            Data = await _service.UpdateAsync(dto)
        });
}
