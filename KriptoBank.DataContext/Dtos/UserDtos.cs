using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KriptoBank.DataContext.Dtos
{
    public class UserRegistrationDto
    {
        [Required(ErrorMessage ="Felhasználónév megadása kötelező")]
        public string Username { get; set; }
        [Required(ErrorMessage ="Jelszó megadása kötelező")]
        [MinLength(6,ErrorMessage ="Legalább 6 karakter kell hogy legyen")]
        public string Password { get; set; }
        [Required(ErrorMessage ="Emailcím megadása kötelező")]
        [EmailAddress(ErrorMessage ="Emailcím formátum kötelező")]
        public string Email { get; set; }
    }

    public class UserUpdatePasswordDto
    {
        [Required(ErrorMessage = "Felhasználónév megadása kötelező")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Jelszó megadása kötelező")]
        [MinLength(6, ErrorMessage = "Legalább 6 karakter kell hogy legyen")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Emailcím megadása kötelező")]
        [EmailAddress(ErrorMessage = "Emailcím formátum kötelező")]
        public string Email { get; set; }
    }
    public class UserDataDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
