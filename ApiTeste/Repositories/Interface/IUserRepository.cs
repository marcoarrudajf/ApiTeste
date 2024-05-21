using ApiTeste.Domain;
namespace ApiTeste.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<List<UserModel>> GetUsers(List<int> ids);
        Task<UserModel> GetUser(int id);
        Task<bool> CpfExisting(string cpf);
        Task<int> AddUser(UserModel user);
        Task<bool> UpdateUser(int id, UserModel user);
        Task<bool> DeleteUser(int id);

    }
}
