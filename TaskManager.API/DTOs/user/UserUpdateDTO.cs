using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace TaskManager.API.DTOs.user
{
    public class UserUpdateDTO
    {
        [MaxLength(100)]
        [SwaggerSchema(Description = "Nome")]
        public string? Name { get; set; }

        [EmailAddress]
        [SwaggerSchema(Description = "Email")]
        public string? Email { get; set; }
    }
}