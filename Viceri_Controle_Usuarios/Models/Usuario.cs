﻿using System.ComponentModel.DataAnnotations;

namespace Viceri_Controle_Usuarios.Models
{
    public class Usuario
    {
        public int Id { get; set; } 
        [Required]
        public string Nome { get; set; } = string.Empty;
        [Required]
        public string Senha { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Cpf { get; set; } = string.Empty;
        [Required]
        public string DataNasc { get; set; } = string.Empty;
    }
}
