using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TheEasyBazar.Data.IRepositories;
using TheEasyBazar.Domain.Entites;
using TheEasyBazar.Service.DTOs.Users;
using TheEasyBazar.Service.Exceptions;
using TheEasyBazar.Service.Interfaces;

namespace TheEasyBazar.Service.Services;

public class UserService : IUserService
{
    private readonly IRepository<User> _repository;
    private readonly IMapper _mapper;
    public UserService(IRepository<User> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<UserForResultDto> CreateAsync(UserForCreationDto dto)
    {
        var user = await _repository.SelectAllAsync()
            .FirstOrDefaultAsync(x => x.Email.ToLower() == dto.Email.ToLower());

        if (user != null)
            throw new CustomException(409, "User already exists..");

        var mappedUser = _mapper.Map<User>(dto);
        mappedUser.CreatedAt = DateTime.UtcNow;
        var result = await _repository.InsertAsync(mappedUser);

        return _mapper.Map<UserForResultDto>(result);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var user = await _repository.SelectByIdAsync(id);
        if (user is null)
            throw new CustomException(404, "User not found");

        await _repository.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<UserForResultDto>> RetrieveAllAsync()
    {
        var users = await _repository.SelectAllAsync().ToListAsync();

        return _mapper.Map<IEnumerable<UserForResultDto>>(users);
    }

    public async Task<UserForResultDto> RetrieveByIdAsync(long id)
    {
        var user = await _repository.SelectByIdAsync(id);
        if (user is null)
            throw new CustomException(404, "User not  found");

        return _mapper.Map<UserForResultDto>(user);
    }
    
    public async Task<UserForResultDto> UpdateAsync(UserForUpdateDto dto)
    {
        var user = await _repository.SelectByIdAsync(dto.Id);
        if (user is null)
            throw new CustomException(404, "User Not found");
        var mappedUser = _mapper.Map<User>(dto);

        var result = await _repository.UpdateAsync(mappedUser);
        mappedUser.UpdatedAt = DateTime.UtcNow;
        return _mapper.Map<UserForResultDto>(result);

    }
}
