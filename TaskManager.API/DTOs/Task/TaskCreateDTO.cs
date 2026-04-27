using System;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;
using TaskManager.API.model.enums;

namespace TaskManager.API.DTOs.Task
{
    public class TaskCreateDTO
    {
        [Required]
        [MaxLength(100)]
        [SwaggerSchema("Título da tarefa")]
        public string Title { get; set; }

        [MaxLength(500)]
        [SwaggerSchema("Descrição da tarefa")]
        public string Description { get; set; }

        [Required]
        [SwaggerSchema("Prioridade: 0=Low, 1=Medium, 2=High")]
        public TaskPriority Priority { get; set; }

        [SwaggerSchema("Data limite")]
        public DateTime? DueDate { get; set; }

        [Required]
        [SwaggerSchema("ID do usuário")]
        public int UserId { get; set; }
    }
}