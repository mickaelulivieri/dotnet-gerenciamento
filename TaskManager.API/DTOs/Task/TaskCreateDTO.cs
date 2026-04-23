using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.model.enums;

namespace TaskManager.API.DTOs.Task
{
    public class TaskCreateDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskPriority Priority { get; set; }
        public DateTime? DueDate { get; set; }
        public int UserId { get; set; }
    }
}