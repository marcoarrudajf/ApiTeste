using ApiTeste.Domain;
using ApiTeste.Repositories.Interface;
using ApiTeste.Services.Interface;
using Org.BouncyCastle.Crypto;

namespace ApiTeste.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserDto> GetUser(int id)
        {
            var user = await _repository.GetUser(id);

            if (user == null)
            {
                return null;
            }

            return new UserDto()
            {
                Name = user.Name,
                SobreNome = user.SobreNome,
                Cpf = user.Cpf,
                Function = user.Function,
                Idade = user.Idade,
                Salario = user.Salario

            };
        }
        public async Task<List<UserDto>> GetUsers(List<int>? ids = null)
        {
            var users = await _repository.GetUsers(ids);

            if (users == null || !users.Any())
            {
                return null;
            }
            return users.Select(user => new UserDto()
            {
                Name = user.Name,
                SobreNome = user.SobreNome,
                Cpf = user.Cpf,
                Function = user.Function,
                Idade = user.Idade,
                Salario = user.Salario
            }).ToList();
        }

        public async Task<int> AddUser(UserDto user)
        {      
            var full = user.GetFullName();
            if (!await _repository.CpfExisting(user.Cpf))
            {                 
                var idUser = await _repository.AddUser(new UserModel()
                {
                    Name = user.Name,
                    SobreNome = user.SobreNome,
                    Cpf = user.Cpf,
                    Function = user.Function,
                    Idade = user.Idade,
                    Salario = user.Salario
                });
                return idUser;
            }
            else
            {
                return 0;
            }
        }

        public async Task<bool> UpdateUser(int id, UserDto user)
        {
            var userResult = await _repository.GetUser(id);
            if (userResult == null)
            {
                return false;
            }

            var success = await _repository.UpdateUser(id, new UserModel
            {
                Name = user.Name,
                SobreNome = user.SobreNome,
                Cpf = user.Cpf,
                Function = user.Function,
                Idade = user.Idade,
                Salario = user.Salario
            });

            return success;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var userResult = await _repository.GetUser(id);
            if (userResult == null)
            {
                return false;
            }
            var success = await _repository.DeleteUser(id);

            return success;
        }
    }
}
