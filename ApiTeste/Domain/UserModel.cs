using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ApiTeste.Domain
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SobreNome { get; set; }
        public string Cpf { get; set; }
        public string Function { get; set; }
        public int Idade { get; set; }
        public double Salario { get; set; }               
    }
}
