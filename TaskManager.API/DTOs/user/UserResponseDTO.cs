using Swashbuckle.AspNetCore.Annotations;

namespace TaskManager.API.DTOs
{
    public class UserResponseDTO
    {
        [SwaggerSchema(Description = "ID")]
        public int Id { get; set; }

        [SwaggerSchema(Description = "Nome")]
        public string Name { get; set; }

        [SwaggerSchema(Description = "Email")]
        public string Email { get; set; }
    }
}