using Star.WebApp.MVC.Extensions.Annotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Star.WebApp.MVC.Models.User
{
    public class UserRegister
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Nome Completo")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("CPF")]
        [Cpf]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "As senhas não conferem.")]
        public string PasswordConfirmed { get; set; }
    }
}