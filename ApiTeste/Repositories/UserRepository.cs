using ApiTeste.Domain;
using ApiTeste.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiTeste.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MeuDbContext _context;

        public UserRepository(MeuDbContext context)
        {
            _context = context;
        }
        public Task<List<UserModel>> GetUsers(List<int>? ids = null)
        {
            IQueryable<UserModel> query = _context.pessoa;

            if (ids != null && ids.Count > 0)
            {
                query = query.Where(u => ids.Contains(u.Id));
            }
            return query.ToListAsync();
        }

        public async Task<UserModel> GetUser(int id)
        {
            return await _context.pessoa.FindAsync(id);
        }

        public async Task<bool> CpfExists(string cpf)
        {
            return await _context.pessoa.AnyAsync(u => u.Cpf == cpf);
        }

        public async Task<int> AddUser(UserModel user)
        {
            _context.pessoa.Add(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }

        public async Task<bool> UpdateUser(int id, UserModel user)
        {
            var existingUser = await _context.pessoa.FindAsync(id);
            if (existingUser == null)
            {
                return false;
            }

            existingUser.Name = user.Name;
            existingUser.SobreNome = user.SobreNome;
            existingUser.Cpf = user.Cpf;
            existingUser.Function = user.Function;
            existingUser.Idade = user.Idade;
            existingUser.Salario = user.Salario;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _context.pessoa.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            _context.pessoa.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
