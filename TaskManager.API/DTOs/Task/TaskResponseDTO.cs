using System;
using Swashbuckle.AspNetCore.Annotations;
using TaskManager.API.model.enums;

namespace TaskManager.API.DTOs.Task
{
    public class TaskResponseDTO
    {
        [SwaggerSchema(Description = "ID da tarefa")]
        public int Id { get; set; }

        [SwaggerSchema(Description = "Título")]
        public string Title { get; set; }

        [SwaggerSchema(Description = "Descrição")]
        public string Description { get; set; }

        [SwaggerSchema(Description = "Status: 0=Pending, 1=InProgress, 2=Completed")]
        public TaskStatus Status { get; set; }

        [SwaggerSchema(Description = "Prioridade")]
        public TaskPriority Priority { get; set; }

        [SwaggerSchema(Description = "Data de criação")]
        public DateTime CreatedAt { get; set; }

        [SwaggerSchema(Description = "Data limite")]
        public DateTime? DueDate { get; set; }

        [SwaggerSchema(Description = "ID do usuário")]
        public int UserId { get; set; }
    }
}