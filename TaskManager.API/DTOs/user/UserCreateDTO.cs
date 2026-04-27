using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace TaskManager.API.DTOs
{
    public class UserCreateDTO
    {
        [Required]
        [MaxLength(100)]
        [SwaggerSchema(Description = "Nome do usuário")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [SwaggerSchema(Description = "Email do usuário")]
        public string Email { get; set; }
    }
}