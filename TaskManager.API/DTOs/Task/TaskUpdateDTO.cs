using System;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;
using TaskManager.API.model.enums;

namespace TaskManager.API.DTOs.Task
{
    public class TaskUpdateDTO
    {
        [MaxLength(100)]
        [SwaggerSchema(Description = "Título")]
        public string? Title { get; set; }

        [MaxLength(500)]
        [SwaggerSchema(Description = "Descrição")]
        public string? Description { get; set; }

        [SwaggerSchema(Description = "Status")]
        public TaskStatus? Status { get; set; }

        [SwaggerSchema(Description = "Prioridade")]
        public TaskPriority? Priority { get; set; }

        [SwaggerSchema(Description = "Data limite")]
        public DateTime? DueDate { get; set; }
    }
}