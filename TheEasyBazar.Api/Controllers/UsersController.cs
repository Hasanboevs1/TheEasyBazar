using Microsoft.AspNetCore.Mvc;
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
    public async Task<List<UserForResultDto>> GetAll()
    {
        var users = await _service.RetrieveAllAsync();

        return users.ToList();
    }

    [HttpGet("id")]
    public async Task<UserForResultDto> GetById(long id)
    {
        var user = await _service.RetrieveByIdAsync(id);
        return user;
    }

    [HttpPost]
    public async Task<UserForResultDto> Create(UserForCreationDto dto)
    {
        var user = await _service.CreateAsync(dto);
        return user;
    }

    [HttpDelete("id")]
    public async Task<bool> Delete(long id)
    {
        var user = await _service.RemoveAsync(id);
        return user;
    }

    [HttpPut]
    public async Task<UserForResultDto> Update(UserForUpdateDto dto)
    {
        var user = await _service.UpdateAsync(dto);
        return user;
    }
}
