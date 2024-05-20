using ApiTeste.Domain;

namespace ApiTeste.Services.Interface
{
    public interface IUserService
    {
        Task<UserDto> GetUser(int id);
        Task<List<UserDto>> GetUsers(List<int> ids = null);
        Task<int> AddUser(UserDto user);
        Task<bool> UpdateUser(int id, UserDto user);
        Task<bool> DeleteUser(int id);
    }
}
