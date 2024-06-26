﻿namespace ApiTeste.Domain
{
    public class UserDto
    {
        public string Name { get; set; }
        public string SobreNome { get; set; }
        public string Cpf { get; set; }
        public string Function { get; set; }
        public int Idade { get; set; }
        public double Salario { get; set; }
    }

    public static class UserHelper
    {
        public static string GetFullName(this UserDto userDto)
        => userDto.Name + userDto.SobreNome;
    }

}
